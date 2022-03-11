using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionModule : Module
{
    public InteractiveObject currentObject;

    [SerializeField] private InteractiveModule[] allInteractions;
    
    public override void LinkModule()
    {
        Debug.Log("Linking Inputs for Interaction Module");
    
        PlayerController.instance.playerActions.Player.Interact.performed += context => InputPressed(context);
        PlayerController.instance.playerActions.Player.Interact.canceled += context => InputReleased(context);
        PlayerController.instance.playerActions.Player.EndInteract.performed += context => StopInteract();
    }
    
    private void StopInteract()
    {
        if (currentObject != null && currentObject.isActive)
        {
            currentObject.isActive = false;
        }
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
        
        // Check s'il y a un objet à proximité

        if (PlayerController.instance.nearestObject == null)
        {
            return false;
        }

        return true;
    }
        
    public override void Execute()
    {
        if (!isPerformed)
        {
            StartCoroutine(InteractingWithObject());
        }
    }
    
    IEnumerator InteractingWithObject()
    {
        SettingCurrentObject();
        
        isPerformed = true;
        
        yield return new WaitWhile(() => currentObject.isActive);

        currentObject = null;
        foreach (var interaction in allInteractions)
        {
            interaction.interactiveObject = null;
        }
        yield return new WaitForSeconds(0.25f);
        isPerformed = false;
    }

    private void SettingCurrentObject()
    {
        Component[] components = PlayerController.instance.nearestObject.GetComponents(typeof(InteractiveObject));
        currentObject = components[0].GetComponent<InteractiveObject>();
        
        var transform1 = transform;
        transform1.position = currentObject.playerTransform.position;
        transform1.eulerAngles = currentObject.playerTransform.eulerAngles;

        foreach (var interaction in allInteractions)
        {
            if (interaction.typeInfo == currentObject.GetType())
            {
                interaction.interactiveObject = currentObject;
            }
        }
        
        currentObject.isActive = true;
    }
    
    public override void Release()
    {
            
    }
}
