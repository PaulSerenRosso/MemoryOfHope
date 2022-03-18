using UnityEngine;
using UnityEngine.InputSystem;

// S'il y a un objet ciblé dans MoveObjectModule, alors on peut le sélectionner et le déplacer
public class MoveObjectFunction : Module
{
    [SerializeField] private MoveObjectModule moveObjectModule;
    [SerializeField] private MoveObjectData data;
    [SerializeField] private Vector3 joystickDirection;
    private Vector3 moveVector;
    
    public override void LinkModule()
    {
        Debug.Log("Linking Inputs for Moving Function");
        
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

        if (moveObjectModule.selectedObject != null) return true;
        
        return false;
    }

    public override void Execute()
    {
        moveVector = new Vector3(joystickDirection.x, 0, joystickDirection.y);
        
        moveVector.Normalize();
        data.rb.velocity = moveVector * data.moveSpeed * Time.fixedDeltaTime;
        Debug.Log(data.rb.velocity);
    }

    public void Select()
    {
        isPerformed = true;
        data = moveObjectModule.selectedObject.GetComponent<MoveObjectData>();
        data.GetComponent<Renderer>().material = data.selectedMaterial;
        
        // Selection feedbacks
        
        data.rb.isKinematic = false;
    }

    public void Deselect()
    {
        if (data != null)
        {
            data.GetComponent<Outline>().enabled = false;
            data.GetComponent<Renderer>().material = data.defaultMaterial;
            data.rb.isKinematic = true;
        }
        
        // Deselection feedbacks
        
        isPerformed = false;
    }
    
    public override void Release()
    {
        
    }
}
