using UnityEngine;
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
    public override void LinkModule()
    {
        // joystick
        // bouton qui maintient
        // tu ne bouge tu ne peux faire aucune input pendant ce temps
        PlayerController.instance.playerActions.Player.Move.performed += context => JoystickPressed(context);
        PlayerController.instance.playerActions.Player.Prism.canceled += context => InputReleased(context);
        PlayerController.instance.playerActions.Player.Prism.performed += context => InputPressed(context);
    }

    public override bool Conditions()
    {
        if (!base.Conditions()) return false;
        
        if (PlayerManager.instance.isHit)
        {
            return false; // Ne peut pas faire le prisme si le joueur est knockback
        }
        
        if (!PlayerController.instance.onGround || _shield.isDead) return false;
        return true;
    }

    public override void InputPressed(InputAction.CallbackContext ctx)
    {
        inputPressed = true;
    }

    public void JoystickPressed(InputAction.CallbackContext ctx)
    {
        _joystickDirection = ctx.ReadValue<Vector2>();
    }

    public override void InputReleased(InputAction.CallbackContext ctx)
    {
        inputPressed = false;
        Release();
    }

    public override void Execute()
    {
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
            if (_shield.isDead)
            {
                Release();
                return;
            }
            _shield.InputShield = true;
            if (_joystickDirection == Vector3.zero)
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
                Vector3.RotateTowards(angleFoward, inputCam.normalized, rotationSpeed, 00f);
            PlayerController.instance.playerRb.rotation =
                Quaternion.Euler(Vector3.up * Mathf.Atan2(rotationVector.x, rotationVector.y) * Mathf.Rad2Deg);
        }
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