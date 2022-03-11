using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GlitchModule : Module
{
    [SerializeField] Glitch glitch;
    
    public override void LinkModule()
    {
        Debug.Log("Linking Inputs for Glitch Module");

        PlayerController.instance.playerActions.Player.Glitch.performed += context => InputPressed(context);
        PlayerController.instance.playerActions.Player.Glitch.canceled += context => InputReleased(context);
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
    
    public override bool Conditions()
    {
        if (!base.Conditions())
        { 
            return false;
        }

        if (!PlayerManager.instance.hasGlitch)
        {
            return false; // Pas de glitch disponibles
        }
        
        return true;
    }
    
    public override void Execute()
    {
        if (!isPerformed)
        {
            StartCoroutine(PerformingGlitch());
        }
    }

    IEnumerator PerformingGlitch()
    {
        isPerformed = true;

        PlayerManager.instance.hasGlitch = false;
        StartCoroutine(glitch.Execute());

        yield return new WaitWhile(() => glitch.isBeingUsed);
        
        isPerformed = false;
    }

    public override void Release()
    {
        
    }
}

