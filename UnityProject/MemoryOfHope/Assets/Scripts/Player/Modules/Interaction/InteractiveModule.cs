using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractiveModule : Module
{
    public Type typeInfo;
    public InteractiveObject interactiveObject;
    public InteractionType interactionType;
    public override void LinkModule()
    {
        
    }
    
    public override void InputPressed(InputAction.CallbackContext ctx)
    {
        
    }
    
    public override void InputReleased(InputAction.CallbackContext ctx)
    {
        
    }
        
    public override bool Conditions()
    {
        if (!base.Conditions())
        {
            return false;
        }

        // Check du type d'interaction
        if (interactiveObject == null || interactiveObject.interactionType != interactionType)
        {
            return false;
        }

        return true;
    }
        
    public override void Execute()
    {

    }
    

    public override void Release()
    {
        
    }
}
