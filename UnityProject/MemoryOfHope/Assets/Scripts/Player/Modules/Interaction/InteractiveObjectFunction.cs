using UnityEngine;
using UnityEngine.InputSystem;

public class InteractiveObjectFunction : Module
{
    public InteractionType type;
    public bool isSelected;
    public InteractionModule interactionModule;
    public Vector3 joystickDirection;
    public Vector3 moveVector;
    public Vector2 inputCam;

    public override void LinkModule()
    {
        PlayerController.instance.playerActions.Player.Move.performed += context => InputPressed(context);
        PlayerController.instance.playerActions.Player.Move.canceled += context => InputReleased(context);
    }
    
    public override void InputPressed(InputAction.CallbackContext ctx)
    {
        inputPressed = true;
        joystickDirection = ctx.ReadValue<Vector2>();
    }
    
    public override void InputReleased(InputAction.CallbackContext ctx)
    {
        inputPressed = false;
        Release();
    }

    public override bool Conditions()
    {
        if (!base.Conditions()) return false;

        if (interactionModule.selectedObject != null && isSelected) return true;
        
        return false;
    }

    public override void Execute()
    {
        
    }

    public virtual void Select()
    {
        isPerformed = true;
        isSelected = true;
        PlayerController.instance.playerRb.isKinematic = true;
    }

    public virtual void Deselect()
    {
        PlayerController.instance.playerRb.isKinematic = false;
        isSelected = false;
        isPerformed = false;
    }
    
    public override void Release()
    {
        
    }
}
