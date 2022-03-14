using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpModule : Module
{
    [SerializeField] private float gravityJump;

//velocity.y += Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
    private float yStartPosition;
    private float yEndMinPosition;

    float currentSpeed = 0;
    [SerializeField] private MoveModule moveModule;
    [SerializeField] private float HeightJump;
    [SerializeField] private float inputMaxTime;
    private float inputTimer;
    private AnimationCurve curveJumpSpeed;
    bool inExecute;
    private float yCurrentEndMaxPosition;
    private bool isRelease = true;

    public override void LinkModule()
    {
        Debug.Log("Linking Inputs for Jump Module");

        PlayerController.instance.playerActions.Player.Jump.performed += context => InputPressed(context);
        PlayerController.instance.playerActions.Player.Jump.canceled += context => InputReleased(context);
    }


    public override bool Conditions()
    {
        if (!base.Conditions()) return false;
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
                yEndMinPosition = yStartPosition + HeightJump;
                PlayerController.instance.stuckGround = false;
                isPerformed = true;
                isRelease = false;
                PlayerController.instance.currentGravity = gravityJump;
                currentSpeed = Mathf.Sqrt(-2f * -PlayerController.instance.currentGravity * HeightJump);
                ;

                PlayerController.instance.currentVelocity += currentSpeed * Vector3.up;
                if (moveModule.inputPressed)
                {
                    PlayerController.instance.currentVelocity += moveModule.moveVector;
                }
            }
            if (inputTimer < inputMaxTime) inputTimer += Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (isPerformed)
        {
            if (PlayerController.instance.playerRb.position.y >= yEndMinPosition)
            {
                inputTimer = 0;
                inExecute = false;
                PlayerController.instance.playerAnimator.SetBool("jumpAir", false);
                PlayerController.instance.currentGravity = PlayerController.instance.defaultGravity;
                PlayerController.instance.stuckGround = true;
                isPerformed = false;
                yCurrentEndMaxPosition = 0;
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
        if (!isPerformed)
        {
            Debug.Log("testqdfqsfsqdfq");
            inputTimer = 0;
        }

        inExecute = false;
    }
}