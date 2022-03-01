using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpModule : Module
{

    [SerializeField] private float gravityJump;
//velocity.y += Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
    [SerializeField]
    private float maxHeightJump;
    [SerializeField]
    private float speedJump;
    private float yStartPosition;
    private float yEndMinPosition;

    [SerializeField]
    private float minHeightJump;
    [SerializeField]
    private float inputMaxTime;
    [SerializeField]
    private float inputMinTime;
    private float inputTimer;

   bool inExecute;
    private float yCurrentEndMaxPosition;
    
    public override void LinkModule()
    {
        Debug.Log("Linking Inputs for Jump Module");

        PlayerController.instance.playerActions.Player.Jump.performed += context => InputPressed(context);
        PlayerController.instance.playerActions.Player.Jump.canceled += context => InputReleased(context);
    }

    public override bool Conditions()
    {
        if (!base.Conditions()) return false;
        if (PlayerController.instance.onGround || !PlayerController.instance.onGround && isPerformed) 
        return true;
      
        return false;
    }

    public override void InputPressed(InputAction.CallbackContext ctx)
    {
        inputPressed = true; 
    }

    void Update()
    {
        if (inExecute && yCurrentEndMaxPosition == 0 )
        {
            if (PlayerController.instance.onGround && !isPerformed && inputTimer > inputMinTime)
            {
                PlayerController.instance.playerAnimator.SetBool("jumpAir", true);
                yStartPosition = PlayerController.instance.playerRb.position.y;
                yEndMinPosition = yStartPosition + minHeightJump;
                PlayerController.instance.stuckGround = false;
                isPerformed = true; 
                
            }  
            if(inputTimer < inputMaxTime) inputTimer += Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        if (isPerformed)
        {
             if (PlayerController.instance.playerRb.position.y <= yEndMinPosition)
                        {
                            float currentSpeed = speedJump - PlayerController.instance.playerRb.velocity.y;
                            PlayerController.instance.currentVelocity += currentSpeed*Vector3.up;
                        }
             else if( inputTimer > inputMinTime) 
             {
                 if (yCurrentEndMaxPosition == 0)
                 {
                     float factorTime = inputTimer/ inputMaxTime;
                     yCurrentEndMaxPosition = factorTime*maxHeightJump+yStartPosition;
                 }
                 else if (PlayerController.instance.playerRb.position.y <= yCurrentEndMaxPosition)
                 {
                     float currentSpeed = speedJump - PlayerController.instance.playerRb.velocity.y;
                     PlayerController.instance.currentVelocity += currentSpeed*Vector3.up;
                 }
                 else
                 {
                     inputTimer = 0;
                     inExecute = false;
                     PlayerController.instance.playerAnimator.SetBool("jumpAir", false);
                     PlayerController.instance.currentGravity = 0; 
                     PlayerController.instance.stuckGround = true;
                     isPerformed = false;
                     yCurrentEndMaxPosition = 0;
                 }
             }
             else
             {
                 inputTimer = 0;
                 inExecute = false;
                 PlayerController.instance.playerAnimator.SetBool("jumpAir", false);
                 PlayerController.instance.currentGravity = 0;
                 PlayerController.instance.stuckGround = true;
                 isPerformed = false;
             }
        }
    }

    public override void InputReleased(InputAction.CallbackContext ctx)
    {
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
            inputTimer = 0; 
        }
        inExecute = false;
    }
    
    
}
