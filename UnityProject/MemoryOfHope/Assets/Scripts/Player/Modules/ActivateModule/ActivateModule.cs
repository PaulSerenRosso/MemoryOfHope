using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateModule : Module
{
    public override bool Conditions()
    {
        if (!base.Conditions()) return false;
        if (PlayerManager.instance.CurrentListenerActivate == null) return false;
        if (PlayerManager.instance.CurrentListenerActivate.isActivate) return false;
        return true;
    }

    public override void LinkModule()
    {
        PlayerController.instance.playerActions.Player.Activate.performed += context => InputPressed(context);
        PlayerController.instance.playerActions.Player.Activate.canceled += context => InputReleased(context);
    }

    public override void InputPressed(InputAction.CallbackContext ctx)
    {
        inputPressed = true;
    }

    public override void InputReleased(InputAction.CallbackContext ctx)
    {
        inputPressed = false;
    }

    public override void Execute()
    {
        isPerformed = true;
        PlayerManager.instance.CurrentListenerActivate.Activate();
        isPerformed = false;
    }

    public override void Release()
    {
        
    }
}
