using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveModule : Module
{
    public Rigidbody player;
    public Vector2 move;
    public override void LinkModule()
    {
        Debug.Log("Linking Inputs for Move Module");
        player = GetComponent<Rigidbody>();
        PlayerManager.instance.playerActions.Player.Jump.performed += context => InputPressed(context);
        PlayerManager.instance.playerActions.Player.Jump.canceled += context => InputReleased(context);
    }
    
    public override void InputPressed(InputAction.CallbackContext ctx)
    {
        inputPressed = ctx.performed;
        move = ctx.ReadValue<Vector2>();
    }

    public override void InputReleased(InputAction.CallbackContext ctx)
    {
        inputPressed = ctx.performed;
        Release();
    }
    
    public override void Execute()
    {
        if (!isPerformed)
        {
            isPerformed = true;
            player.velocity = move;
            Debug.Log("Moving");

            isPerformed = false;
        }
    }

    public override void Release()
    {
        
    }
    
}
