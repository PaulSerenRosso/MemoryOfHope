using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PrismModule : Module
{
    private Vector3 _joystickDirection;
    [SerializeField] private LaserMachine _playerMirror;
    [SerializeField] private ShieldManager _shield;

    [SerializeField] private float _activationTime;
    private float _timer;
    private bool isActivate;
    [SerializeField] private float rotationSpeed;
    private Vector2 inputCam;
    private bool joystickPressed;

    private bool isTutorial;
    [SerializeField] private TutorialGameEvent prismTutorial;

    [SerializeField] private UnityEvent _prismPerformedEvent;
    public override void LinkModule()
    {
        GameManager.instance.inputs.Player.Move.performed += JoystickPressed;
        GameManager.instance.inputs.Player.Move.canceled += JoystickReleased;
        GameManager.instance.inputs.Player.Prism.canceled += InputReleased;
        GameManager.instance.inputs.Player.Prism.performed += InputPressed;
        isLinked = true;
        isTutorial = true;
        _shield.Mirror.prismObtained = true;
    }

    private void OnDisable()
    {
        UnlinkModule();
    }

    public override void UnlinkModule()
    {
        if (!isLinked) return;
        GameManager.instance.inputs.Player.Move.performed -= JoystickPressed;
        GameManager.instance.inputs.Player.Move.canceled -= JoystickReleased;
        GameManager.instance.inputs.Player.Prism.canceled -= InputReleased;
        GameManager.instance.inputs.Player.Prism.performed -= InputPressed;
    }

    private void JoystickReleased(InputAction.CallbackContext context)
    {
        joystickPressed = false;
    }

    public override void Cancel()
    {
        Release();   
    }

    public override bool Conditions()
    {
        if (!base.Conditions()) return false;

        if (PlayerManager.instance.isHit)
        {
            Release();
            return false; // Ne peut pas faire le prisme si le joueur est knockback
        }

        if (!PlayerController.instance.onGround || (_shield.isDead && !isActivate)) return false;
        return true;
    }

    public override void InputPressed(InputAction.CallbackContext ctx)
    {
        inputPressed = true;
    }

    public void JoystickPressed(InputAction.CallbackContext ctx)
    {
        joystickPressed = true;
        _joystickDirection = ctx.ReadValue<Vector2>();
    }

    public override void InputReleased(InputAction.CallbackContext ctx)
    {
        inputPressed = false;
        Release();
    }

    public override void Execute()
    {
        if (isTutorial && _shield.Mirror.LaserLineReceiver != null)
        {
            isTutorial = false;
            prismTutorial.RemoveTutorial();
        }
        
        if (_shield.isDead)
        {
            Release();

            return;
        }

        if (!isPerformed)
        {
            _prismPerformedEvent?.Invoke();
        }
        isPerformed = true;

        
        PlayerController.instance.playerAnimator.SetBool("InPrism", true);

        isActivate = true;
        _shield.InputShield = true;

        if (!joystickPressed)
            return;
        Vector2 angleFoward = new Vector2(transform.forward.x,
            transform.forward.z);
        Vector2 _cameraForwardXZ;
        Vector2 _cameraRightXZ;
        _cameraForwardXZ = new Vector3(MainCameraController.Instance.transform.forward.x,
            MainCameraController.Instance.transform.forward.z).normalized;
        _cameraRightXZ = new Vector3(MainCameraController.Instance.transform.right.x,
            MainCameraController.Instance.transform.right.z).normalized;
        inputCam = _cameraForwardXZ * _joystickDirection.y +
                   _cameraRightXZ * _joystickDirection.x;
        Vector2 rotationVector =
            Vector3.RotateTowards(angleFoward, inputCam, rotationSpeed, 00f);
        PlayerController.instance.transform.rotation =
            Quaternion.Euler(Vector3.up * Mathf.Atan2(rotationVector.x, rotationVector.y) * Mathf.Rad2Deg);
    }

    public override void Release()
    {
        _timer = 0;
        isActivate = false;
        isPerformed = false;
        PlayerController.instance.playerAnimator.SetBool("InPrism", false);
        _shield.InputShield = false;
        //disappears du bouclier 
        // temps avant de perdre le controle
    }
}