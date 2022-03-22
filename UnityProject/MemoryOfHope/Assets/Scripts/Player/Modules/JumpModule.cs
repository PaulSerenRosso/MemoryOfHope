using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpModule : Module
{
    //velocity.y += Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
    private float yStartPosition;
    private float yEndPosition;

    float currentSpeed = 0;
    
    [SerializeField] private MoveModule moveModule;
    [SerializeField] private float HeightJump;
    private AnimationCurve curveJumpSpeed;
    bool inExecute;
    private bool isRelease = true;
    [SerializeField]
    private float MaxSpeedJump;
    [SerializeField] private float speedEndJump;

    [SerializeField]
    private AnimationCurve CurveJump;

    public override void LinkModule()
    {
        Debug.Log("Linking Inputs for Jump Module");

        PlayerController.instance.playerActions.Player.Jump.performed += context => InputPressed(context);
        PlayerController.instance.playerActions.Player.Jump.canceled += context => InputReleased(context);
        
        // Set Constraining Modules
        constrainingModules.Clear();
        
        // Set other variables referring to the player
        if(GetComponent<MoveModule>()) moveModule = GetComponent<MoveModule>();
    }


    public override bool Conditions()
    {
        if (!base.Conditions()) return false;

        if (PlayerManager.instance.isHit)
        {
            return false; // Ne peut pas sauter si le joueur est knockback
        }
        
        if ((PlayerController.instance.onGround && isRelease) || !PlayerController.instance.onGround && isPerformed)
            return true;


        return false;
    }

    public override void InputPressed(InputAction.CallbackContext ctx)
    {
        if (isPerformed || PlayerController.instance.onGround)
            inputPressed = true;
    }

    void Update()
    {
        if (inExecute)
        {
            if (PlayerController.instance.onGround && !isPerformed)
            {
                PlayerController.instance.playerAnimator.SetBool("jumpAir", true);
                yStartPosition = PlayerController.instance.playerRb.position.y;
                yEndPosition = yStartPosition + HeightJump;
                PlayerController.instance.stuckGround = false;
                isPerformed = true;
                isRelease = false;
                PlayerController.instance.currentGravity = 0;
                if (moveModule.inputPressed)
                {
                    PlayerController.instance.currentVelocity += PlayerController.instance.PlayerProjectOnPlane(moveModule.moveVector);
                   Debug.Log(moveModule.moveVector);
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (isPerformed)
        {
            if (PlayerController.instance.playerRb.position.y >= yEndPosition)
            {
                Debug.Log(yEndPosition);
                inExecute = false;
                PlayerController.instance.playerAnimator.SetBool("jumpAir", false);
                PlayerController.instance.currentGravity = PlayerController.instance.defaultGravity;
                PlayerController.instance.stuckGround = true;
                PlayerController.instance.currentVelocity += speedEndJump * Vector3.up;
                isPerformed = false;
            }
            else
            {
                Debug.Log(PlayerController.instance.playerRb.position.y-yStartPosition);
                currentSpeed = CurveJump.Evaluate(PlayerController.instance.playerRb.position.y-yStartPosition / yEndPosition) * MaxSpeedJump;
                PlayerController.instance.currentVelocityWithUndo += currentSpeed * Vector3.up;
                PlayerController.instance.currentGravity = 0;
            }
       
        }
    }

    public override void InputReleased(InputAction.CallbackContext ctx)
    {
        isRelease = true;
        inputPressed = false;
        Release();
    }

    public override void Execute()
    {
        inExecute = true;
    }

    public override void Release()
    {
        inExecute = false;
    }
}