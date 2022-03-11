using UnityEngine;
using UnityEngine.InputSystem;

public class RotateObjectFunction : InteractiveModule
{
    private float rotateInput;
    public override void LinkModule()
    {
        typeInfo = typeof(RotateObjectInfo);
        interactionType = InteractionType.Rotate;
        PlayerController.instance.playerActions.Player.RotateInteract.performed += context => InputPressed(context);
        PlayerController.instance.playerActions.Player.RotateInteract.canceled += context => InputReleased(context);
    }
    
    public override void InputPressed(InputAction.CallbackContext ctx)
    {
        inputPressed = ctx.performed;
        rotateInput = ctx.ReadValue<float>();
    }
    
    public override void InputReleased(InputAction.CallbackContext ctx)
    {
        inputPressed = ctx.performed;
        Release();
    }
    
    public override void Execute()
    {
        var data = (RotateObjectInfo) interactiveObject;

        interactiveObject.motherObject.transform.eulerAngles += Vector3.up * (rotateInput * data.speed * Time.deltaTime);
    }
}