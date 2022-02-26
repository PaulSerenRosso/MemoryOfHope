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
    private float yEndPosition;
    private bool readyToJump; 
  
    
    public override void LinkModule()
    {
        Debug.Log("Linking Inputs for Jump Module");

        PlayerController.instance.playerActions.Player.Jump.performed += context => InputPressed(context);
        PlayerController.instance.playerActions.Player.Jump.canceled += context => InputReleased(context);
    }

    public override bool Conditions()
    {
        if (!base.Conditions()) return false;
        if (PlayerController.instance.onGround || !PlayerController.instance.onGround && readyToJump) 
        return true;
      
        return false;
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
        if (PlayerController.instance.onGround && !readyToJump)
        {
            PlayerController.instance.playerAnimator.SetBool("jumpAir", true);
            yStartPosition = PlayerController.instance.playerRb.position.y;
            yEndPosition = yStartPosition + maxHeightJump;
            PlayerController.instance.stuckGround = false;
            readyToJump = true;

        }
        else
        {
            if(PlayerController.instance.currentGravity != gravityJump)
                    PlayerController.instance.currentGravity = gravityJump;
            
            if (PlayerController.instance.playerRb.position.y > yEndPosition)
            {
               Release();
               return;
                
            }
            float currentSpeed = speedJump - PlayerController.instance.playerRb.velocity.y;
             PlayerController.instance.currentVelocity += currentSpeed*Vector3.up;
            
        }
        isPerformed = true;
    }

    public override void Release()
    {
        PlayerController.instance.playerAnimator.SetBool("jumpAir", false);
        readyToJump = false; 
        isPerformed = false;
        PlayerController.instance.currentGravity = 0; 
        PlayerController.instance.stuckGround = true;
    }
    

    /*public override bool Conditions()
    {
        if (base.Conditions())
        {
            return false;
        }
physic influence = previous rigidbody - current rigibody

currentrb

        return true;
    }*/
}
