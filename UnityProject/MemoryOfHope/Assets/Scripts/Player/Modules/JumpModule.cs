using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class JumpModule : Module
{
    //velocity.y += Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
    private float yStartPosition;
    private float yEndPosition;

    float currentSpeed = 0;

    [SerializeField]
    private UnityEvent _jumpPerformedEvent;
    [SerializeField] private MoveModule moveModule;
    [SerializeField] private float HeightJump;
    private AnimationCurve curveJumpSpeed;
    bool inExecute;
    private bool isRelease = true;
    [SerializeField] private float MaxSpeedJump;
    [SerializeField] private float speedEndJump;

    [SerializeField] private AnimationCurve CurveJump;

    private bool isTutorial;
    [SerializeField] private TutorialGameEvent jumpTutorial;

    public override void LinkModule()
    {
        GameManager.instance.inputs.Player.Jump.performed += InputPressed;
        GameManager.instance.inputs.Player.Jump.canceled += InputReleased;
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
        GameManager.instance.inputs.Player.Jump.performed -= InputPressed;
        GameManager.instance.inputs.Player.Jump.canceled -= InputReleased;
    }

    public override void Cancel()
    {
        inExecute = false;
        PlayerController.instance.currentGravity = PlayerController.instance.defaultGravity;
        PlayerController.instance.stuckGround = true;
        PlayerController.instance.currentVelocity += speedEndJump * Vector3.up;
        isPerformed = false;
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
                yStartPosition = PlayerController.instance.playerRb.position.y;
                yEndPosition = yStartPosition + HeightJump;
                PlayerController.instance.stuckGround = false;
                isPerformed = true;
                isRelease = false;
                PlayerController.instance.currentGravity = 0;
                if (moveModule.inputPressed)
                {
                    PlayerController.instance.currentVelocity +=
                        PlayerController.instance.PlayerProjectOnPlane(moveModule.moveVector);
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
                Cancel();
            }
            else
            {
                currentSpeed =
                    CurveJump.Evaluate((PlayerController.instance.playerRb.position.y - yStartPosition) / HeightJump) *
                    MaxSpeedJump;
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
        if (isTutorial)
        {
            _jumpPerformedEvent?.Invoke();
            isTutorial = false;
            jumpTutorial.RemoveTutorial();
        }

        
        inExecute = true;
    }

    public override void Release()
    {
        inExecute = false;
    }
}