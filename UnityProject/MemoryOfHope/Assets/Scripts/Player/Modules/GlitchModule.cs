using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GlitchModule : Module
{
    private Glitch glitchUsed;
    
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

        if (PlayerManager.instance.allGlitches.Count == 0)
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
        Debug.Log("test");
        UIInstance.instance.SetGlitchesOnDisplay(false);
      //  PlayerController.instance.playerRb.Play("Prism");
        glitchUsed = PlayerManager.instance.allGlitches[0];
        PlayerManager.instance.allGlitches.Remove(PlayerManager.instance.allGlitches[0]);
        StartCoroutine(glitchUsed.Execute());

        yield return new WaitWhile(() => glitchUsed.isBeingUsed);
        
       // PlayerManager.instance.animator.Play("Idle");
        isPerformed = false;
    }

    public override void Release()
    {
        
    }
}

