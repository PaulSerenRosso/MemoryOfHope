using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateModule : Module
{
    public override void Cancel()
    {
        Release();
    }

    public override bool Conditions()
    {
        if (!base.Conditions()) return false;
        if (PlayerManager.instance.CurrentListenerActivate == null) return false;
       
        Debug.Log("bonsoir à tous");
        isPerformed = true;
        return true;
    }

    public override void LinkModule()
    {
        GameManager.instance.inputs.Player.Activate.performed += InputPressed;
        GameManager.instance.inputs.Player.Activate.canceled += InputReleased;
        isLinked = true;
    }

    private void OnDisable()
    {
        UnlinkModule();
    }

    public override void UnlinkModule()
    {
        if (!isLinked) return;
        GameManager.instance.inputs.Player.Activate.performed -= InputPressed;
        GameManager.instance.inputs.Player.Activate.canceled -= InputReleased;
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
    if(!PlayerManager.instance.CurrentListenerActivate)
        return;
        Debug.Log(PlayerManager.instance.CurrentListenerActivate);
        PlayerManager.instance.CurrentListenerActivate.Activate();
 
      PlayerManager.instance.CurrentListenerActivate = null;       
        isPerformed = false;
    }

    public override void Release()
    {
        isPerformed = false;
    }
}
