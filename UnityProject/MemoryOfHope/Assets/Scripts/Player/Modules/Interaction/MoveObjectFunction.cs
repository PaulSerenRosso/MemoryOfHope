using UnityEngine;
using UnityEngine.InputSystem;

public class MoveObjectFunction : InteractiveModule
{
    private float moveInput;
    
    public override void LinkModule()
    {
        typeInfo = typeof(MoveObjectInfo);
        interactionType = InteractionType.Move;
        PlayerController.instance.playerActions.Player.MoveInteract.performed += context => InputPressed(context);
        PlayerController.instance.playerActions.Player.MoveInteract.canceled += context => InputReleased(context);
    }
    
    public override void InputPressed(InputAction.CallbackContext ctx)
    {
        inputPressed = ctx.performed;
        moveInput = ctx.ReadValue<float>();
    }
    
    public override void InputReleased(InputAction.CallbackContext ctx)
    {
        inputPressed = ctx.performed;
        Release();
    }
    
    public override void Execute()
    {
        var data = (MoveObjectInfo) interactiveObject;
        var motherTransform = data.motherObject.transform;
        Transform target;

        if (moveInput > 0 && data.pointAfter != null)
        {
            target = data.pointAfter.transform;
            data.goingForward = true;
        }

        else if (moveInput < 0 && data.pointBefore != null)
        {
            target = data.pointBefore.transform;
            data.goingForward = false;
        }
        else
        {
            target = null;
        }

        if (target != null)
        {
            Vector3 direction = new Vector3(target.position.x - motherTransform.position.x, 0,
                target.position.z - motherTransform.position.z).normalized;

            motherTransform.transform.position += direction * Time.deltaTime * Mathf.Abs(moveInput) * data.speed;
        }
    }
}
