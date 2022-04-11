using UnityEngine;
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

    [Range(2, 10)] public float rayLength;
    [SerializeField] private LayerMask interactiveObjectLayer;

    [SerializeField] private Transform raycastOrigin;
    public InteractiveObjectFunction[] interactiveFunction;
    public LineRenderer line;
    private Vector2 inputCam;
    private bool joystickIsPressed;
    
    public override void LinkModule()
    {
        PlayerController.instance.playerActions.Player.InteractionModule.started += context => InputPressed(context);
        PlayerController.instance.playerActions.Player.InteractionModule.canceled += context => InputReleased(context);
        PlayerController.instance.playerActions.Player.Move.performed += context => Aim(context);
        PlayerController.instance.playerActions.Player.Move.canceled += context => AimCancel(context);
        PlayerController.instance.playerActions.Player.InteractionSelect.started += _ => Selecting();
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

    private void Aim(InputAction.CallbackContext ctx)
    {
        joystickIsPressed = true;

        joystickDirection = ctx.ReadValue<Vector2>();
        
    }

    private void Selecting()
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
                        interaction.Select();
                    }
                }
            }
            else if(selectedObject != null)
            {
                selectedObject = null;
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

        if (!PlayerController.instance.onGround)
        {
            return false;
        }
        
        return true;
    }

    public override void Execute()
    {
        if (selectedObject != null) return;
        isPerformed = true;
            
        if (!isActivate)
        {
            if (_timer == 0)
                PlayerController.instance.playerAnimator.SetBool("InPrism", true);
                
            if (_activationTime > _timer)
                _timer += Time.deltaTime;
            else
            {
                _timer = 0;
                isActivate = true;
            }
        }
        else
        {
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
                Vector3.RotateTowards(angleFoward, inputCam.normalized, rotateSpeed, 00f);
            PlayerController.instance.playerRb.rotation =
                Quaternion.Euler(Vector3.up * Mathf.Atan2(rotationVector.x, rotationVector.y) * Mathf.Rad2Deg);
                
            line.positionCount = 2;
            line.SetPosition(0, raycastOrigin.transform.position);
            line.SetPosition(1,   transform.position + transform.forward * rayLength);
            
            if (Physics.Raycast(raycastOrigin.position, transform.forward, out var hit ,rayLength, interactiveObjectLayer))
            {
                if(currentTargetedObject != null) currentTargetedObject.GetComponent<Outline>().enabled = false;
                line.SetPosition(1,   hit.point);
                currentTargetedObject = hit.transform.gameObject;
                currentTargetedObject.GetComponent<Outline>().enabled = true;
            }
            else
            {
                if(currentTargetedObject != null) currentTargetedObject.GetComponent<Outline>().enabled = false;
                currentTargetedObject = null;
            }
        }
    }
    
    public override void Release()
    {
        _timer = 0;
        isActivate = false;
        isPerformed = false;
        PlayerController.instance.playerAnimator.SetBool("InPrism", false);
        if(currentTargetedObject != null) currentTargetedObject.GetComponent<Outline>().enabled = false;
        selectedObject = null;
        line.positionCount = 0;
        foreach (var interaction in interactiveFunction)
        {
            interaction.Deselect();
        }
    }
}
