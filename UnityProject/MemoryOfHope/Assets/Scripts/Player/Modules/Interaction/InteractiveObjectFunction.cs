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
        if (interactionModule.line.positionCount == 2)
        {
            interactionModule.line.SetPosition(1, interactionModule.selectedObject.transform.position);
        }
        
        var tr = PlayerController.instance.transform;
        tr.LookAt(interactionModule.selectedObject.transform);
        tr.eulerAngles = new Vector3(0, tr.eulerAngles.y, tr.eulerAngles.z);
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
