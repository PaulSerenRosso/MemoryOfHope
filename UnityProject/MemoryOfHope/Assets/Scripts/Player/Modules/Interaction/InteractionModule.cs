using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

// Passe le joueur en mode de sélection et permet de stocker l'objet ciblé
public class InteractionModule : Module
{
    public GameObject currentTargetedObject;
    public GameObject selectedObject;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Vector3 joystickDirection;
    [SerializeField] private float _activationTime;
    private float _timer;
    private bool isActivate;

    [SerializeField] private AudioClip _selectAudioClip;
    [SerializeField] private AudioClip _canSelectAudioClip;
    
 [SerializeField] UnityEvent _interactionPerformedEvent;
    [Range(2, 10)] public float rayLength;
    [SerializeField] private LayerMask interactiveObjectLayer;

    [SerializeField] private Transform raycastOrigin;
    public InteractiveObjectFunction[] interactiveFunction;
    public LineRenderer line;
    private Vector2 inputCam;
    private bool joystickIsPressed;
    public TutorialGameEvent selectionTutorial;

    public Gradient interactionLineGradient;
    public Gradient defaultGradient;
    public Color interactionColor;
    public Color defaultColor;

    private bool isTutorial;
    [SerializeField] private TutorialGameEvent interactionTutorial;
    
    public override void LinkModule()
    {
        GameManager.instance.inputs.Player.InteractionModule.started += InputPressed;
        GameManager.instance.inputs.Player.InteractionModule.canceled += InputReleased;
        GameManager.instance.inputs.Player.Move.performed += Aim;
        GameManager.instance.inputs.Player.Move.canceled += AimCancel;
        GameManager.instance.inputs.Player.InteractionSelect.started += Selecting;
        isLinked = true;
        isTutorial = true;
    }

    private void OnDisable()
    {
        UnlinkModule();
    }

    public override void UnlinkModule()
    {
        if (!isLinked) return;
        GameManager.instance.inputs.Player.InteractionModule.started -= InputPressed;
        GameManager.instance.inputs.Player.InteractionModule.canceled -= InputReleased;
        GameManager.instance.inputs.Player.Move.performed -= Aim;
        GameManager.instance.inputs.Player.Move.canceled -= AimCancel;
        GameManager.instance.inputs.Player.InteractionSelect.started -= Selecting;
    }

    private void AimCancel(InputAction.CallbackContext context)
    {
        joystickIsPressed = false;
    }

    public override void InputPressed(InputAction.CallbackContext ctx)
    {
        inputPressed = true;
    }

    public override void InputReleased(InputAction.CallbackContext ctx)
    {
        inputPressed = false;
        Release();
    }

    public override void Cancel()
    {
        _timer = 0;
        isActivate = false;
        isPerformed = false;
        PlayerController.instance.playerAnimator.SetBool("InPrism", false);
        if (currentTargetedObject != null) currentTargetedObject.GetComponent<Outline>().enabled = false;
        line.positionCount = 0;
        foreach (var interaction in interactiveFunction)
        {
            interaction.Deselect();
        }
    }

    private void Aim(InputAction.CallbackContext ctx)
    {
        joystickIsPressed = true;

        joystickDirection = ctx.ReadValue<Vector2>();
    }

    private void Selecting(InputAction.CallbackContext ctx)
    {
        if (isPerformed)
        {
            if (currentTargetedObject != null && selectedObject == null)
            {
                selectedObject = currentTargetedObject;
                //line.positionCount = 0;
                foreach (var interaction in interactiveFunction)
                {
                    Component component = selectedObject.GetComponent(typeof(InteractiveObjectData));
                    var interactive = (InteractiveObjectData) component;

                    if (interaction.type == interactive.type)
                    {
                        interactive.AudioSource.PlayOneShot(_selectAudioClip);
                        interaction.Select();
                        
                    }
                }
            }
            else if (selectedObject != null)
            {
                foreach (var interaction in interactiveFunction)
                {
                    interaction.Deselect();
                }
            }
        }
    }

    public override bool Conditions()
    {
        if (!base.Conditions())
        {
            return false;
        }

        if (PlayerManager.instance.isHit)
        {
            Release();
            return false; // Ne peut pas faire le prisme si le joueur est knockback
        }

        if (!PlayerController.instance.onGround)
        {
            return false;
        }

        return true;
    }

    public override void Execute()
    {
        if(!isPerformed)
            _interactionPerformedEvent?.Invoke();
        if (isTutorial)
        {
            isTutorial = false;
            interactionTutorial.RemoveTutorial();
        }
        
        if (selectedObject != null) return;

        isPerformed = true;
        isActivate = true;

        PlayerController.instance.playerAnimator.SetBool("InPrism", true);
        
        Vector2 angleFoward = new Vector2(transform.forward.x,
            transform.forward.z);
        Vector2 _cameraForwardXZ;
        Vector2 _cameraRightXZ;
        _cameraForwardXZ = new Vector3(MainCameraController.Instance.transform.forward.x,
            MainCameraController.Instance.transform.forward.z).normalized;
        _cameraRightXZ = new Vector3(MainCameraController.Instance.transform.right.x,
            MainCameraController.Instance.transform.right.z).normalized;
        inputCam = _cameraForwardXZ * joystickDirection.y +
                   _cameraRightXZ * joystickDirection.x;
        Vector2 rotationVector =
            Vector3.RotateTowards(angleFoward, inputCam, rotateSpeed, 00f);
        PlayerController.instance.transform.rotation =
            Quaternion.Euler(Vector3.up * Mathf.Atan2(rotationVector.x, rotationVector.y) * Mathf.Rad2Deg);

        line.positionCount = 2;
        line.SetPosition(0, raycastOrigin.transform.position);
        line.SetPosition(1, transform.position + transform.forward * rayLength);

        if (Physics.Raycast(raycastOrigin.position, transform.forward, out var hit, rayLength, interactiveObjectLayer))
        {
            if (currentTargetedObject != null)
            { currentTargetedObject.GetComponent<Outline>().enabled = false;}
            else
            {
                AudioSource audioSource = hit.transform.GetComponent<InteractiveObjectData>().AudioSource;
                audioSource.PlayOneShot(_canSelectAudioClip);
            }
            selectionTutorial.SetTutorial();
            line.SetPosition(1, hit.point);
            
            currentTargetedObject = hit.transform.gameObject;
            
            currentTargetedObject.GetComponent<Outline>().enabled = true;
            
            GameManager.instance.RumbleConstant(.1f, .1f, .1f);

        }
        else
        {
            if (currentTargetedObject != null) currentTargetedObject.GetComponent<Outline>().enabled = false;
            selectionTutorial.RemoveTutorial();
            currentTargetedObject = null;
        }
    }

    public override void Release()
    {
        _timer = 0;
        isActivate = false;
        isPerformed = false;
        PlayerController.instance.playerAnimator.SetBool("InPrism", false);
        if (currentTargetedObject != null) currentTargetedObject.GetComponent<Outline>().enabled = false;
        line.positionCount = 0;
        foreach (var interaction in interactiveFunction)
        {
            interaction.Deselect();
        }
    }
}