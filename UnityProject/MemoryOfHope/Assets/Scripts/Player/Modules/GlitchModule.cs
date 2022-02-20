using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class GlitchModule : Module
{
    private Glitch glitchUsed;
    
    public override void LinkModule()
    {
        Debug.Log("Linking Inputs for Glitch Module");

        PlayerManager.instance.playerActions.Player.Glitch.performed += context => InputPressed(context);
        PlayerManager.instance.playerActions.Player.Glitch.canceled += context => InputPressed(context);
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
    
    public override bool Conditions()
    {
        if (!base.Conditions())
        {
            return false;
        }

        if (PlayerManager.instance.allGlitches.Count == 0)
        {
            Debug.Log("Aucun glitch disponible");
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
        glitchUsed = PlayerManager.instance.allGlitches[0];

        PlayerManager.instance.allGlitches.Remove(PlayerManager.instance.allGlitches[0]);
        StartCoroutine(glitchUsed.Execute());

        yield return new WaitWhile(() => glitchUsed.isBeingUsed);
        
        isPerformed = false;
    }

    public override void Release()
    {
        
    }
}

