using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpModule : Module
{
    public Rigidbody player;
    public Vector3 jump;
    public override void LinkModule()
    {
        Debug.Log("Linking Inputs for Jump Module");
        player = GetComponent<Rigidbody>();
        PlayerManager.instance.playerActions.Player.Jump.performed += context => InputPressed(context);
        PlayerManager.instance.playerActions.Player.Jump.canceled += context => InputReleased(context);
    }

    public override void InputPressed(InputAction.CallbackContext ctx)
    {
        inputPressed = ctx.performed;
    }

    public override void InputReleased(InputAction.CallbackContext ctx)
    {
        inputPressed = ctx.performed;
        Release();
    }
    
    public override void Execute()
    {
        if (!isPerformed && PlayerManager.instance.canJump)
        {
            isPerformed = true;
            PlayerManager.instance.canJump = false;
            Debug.Log("Jumping");

            player.AddForce(jump);

            isPerformed = false;
        }
    }

    public override void Release()
    {
        
    }
    

    /*public override bool Conditions()
    {
        if (base.Conditions())
        {
            return false;
        }

        return true;
    }*/
}
