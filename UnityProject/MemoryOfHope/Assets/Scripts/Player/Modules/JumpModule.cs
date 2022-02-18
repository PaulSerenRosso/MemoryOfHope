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
        playerActions.actionMaps[0].actions[0].performed += context => InputPressed(context);
        playerActions.actionMaps[0].actions[0].canceled += context => InputReleased(context);
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
    
    private void OnEnable()
    {
        playerActions.Enable();
    }

    private void OnDisable()
    {
        playerActions.Disable();
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
