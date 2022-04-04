using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashModule : Module
{
    [SerializeField] private float timeDash;
    private float timerDash;

    [SerializeField] float speedDash;

    [SerializeField] private float speedRotation;

    private Vector2 joystickVector;
    private bool joystickPressed;
    private int oldHealth;
    private Vector2 inputCam;

    [SerializeField]
   float cooldownDash;

  
  float cooldownDashTimer;

  private bool dashReady;
  

    // Update is called once per frame
    void Update()
    {
        if (!isPerformed)
        {
            if (cooldownDash > cooldownDashTimer)
            {
                cooldownDashTimer += Time.deltaTime;
            }
            else
            {
                   dashReady = true;
            }
            return;
        }

       
        if (oldHealth > PlayerManager.instance.health)
        {
            EndDash();
            return;
        }
     
        if (timerDash < timeDash)
            timerDash += Time.deltaTime;
        else 
            EndDash();
       
    }

    void FixedUpdate()
    {
        
        if (!isPerformed)
            return;
  
    
        PlayerController.instance.currentVelocityWithUndo += transform.forward * speedDash;
        if(joystickPressed)
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
        inputCam = _cameraForwardXZ * joystickVector.y +
                   _cameraRightXZ * joystickVector.x;
        Vector2 rotationVector =
            Vector3.RotateTowards(angleFoward, inputCam.normalized, speedRotation, 00f);
        PlayerController.instance.playerRb.rotation =
            Quaternion.Euler(Vector3.up * Mathf.Atan2(rotationVector.x, rotationVector.y) * Mathf.Rad2Deg);
    }

    public override void LinkModule()
    {
        PlayerController.instance.playerActions.Player.Move.performed += context => JoystickPressed(context);
        PlayerController.instance.playerActions.Player.Move.canceled += context => JoystickReleased(context);
        PlayerController.instance.playerActions.Player.Dash.canceled += context => InputReleased(context);
        PlayerController.instance.playerActions.Player.Dash.performed += context => InputPressed(context);
    }

    public override bool Conditions()
    {
        if (!base.Conditions()) return false;

        if (!dashReady) return false;
        
        if (isPerformed) return false;

        
        return true;
    }

    private void JoystickPressed(InputAction.CallbackContext context)
    {
        joystickVector = context.ReadValue<Vector2>();
        joystickPressed = true;
    }

    private void JoystickReleased(InputAction.CallbackContext context)
    {
        joystickPressed = false;
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

    public override void Execute()
    {
        isPerformed = true;
        oldHealth = PlayerManager.instance.health;
        PlayerController.instance.useGravity = false;
        dashReady = false;
        cooldownDashTimer = 0;
        PlayerController.instance.useCheckGround = false;
        PlayerController.instance.playerRb.velocity = new Vector3(PlayerController.instance.playerRb.velocity.x, 0,
            PlayerController.instance.playerRb.velocity.z);
    }

    public override void Release()
    {
       
    }

     void EndDash()
    {
        timerDash = 0;
        isPerformed = false;
        PlayerController.instance.useGravity = true; 
        PlayerController.instance.useCheckGround = true;
        StartCoroutine(WaitForFirstCheckGround());
    }

     IEnumerator WaitForFirstCheckGround()
     {
         yield return new WaitForFixedUpdate();
         PlayerController.instance.firstCheckGround = true;
     }
    
    
}