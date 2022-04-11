using UnityEngine;
using UnityEngine.InputSystem;

// S'il y a un objet ciblé dans MoveObjectModule, alors on peut le sélectionner et le déplacer
public class MoveObjectFunction : InteractiveObjectFunction
{
    private MoveObjectData data;
    [SerializeField] private float range;
    float leftBound;
    float rightBound;
    float downBound;
    float upBound;
    
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

    public override void Execute()
    {
        base.Execute();

        Vector2 _cameraForwardXZ;
        Vector2 _cameraRightXZ;
        _cameraForwardXZ = new Vector3(MainCameraController.Instance.transform.forward.x,
            MainCameraController.Instance.transform.forward.z).normalized;
        _cameraRightXZ = new Vector3(MainCameraController.Instance.transform.right.x, 
            MainCameraController.Instance.transform.right.z).normalized;
        inputCam = _cameraForwardXZ * joystickDirection.y +
                   _cameraRightXZ * joystickDirection.x;
        moveVector = new Vector3(inputCam.x, 0, inputCam.y);
        moveVector.Normalize();
        
        CheckBoundaries();
        data.rb.velocity = moveVector * data.moveSpeed * Time.fixedDeltaTime;
    }

    void CheckBoundaries()
    {
        Transform dataTransform = data.transform;
        if (dataTransform.position.x < leftBound)
        {
            dataTransform.position = new Vector3(leftBound, dataTransform.position.y, dataTransform.position.z);
        }
        else if (dataTransform.position.x > rightBound)
        {
            dataTransform.position = new Vector3(rightBound, dataTransform.position.y, dataTransform.position.z);
        }

        if (dataTransform.position.z < downBound)
        {
            dataTransform.position = new Vector3(dataTransform.position.x, dataTransform.position.y, downBound);
        }
        else if (dataTransform.position.z > upBound)
        {
            dataTransform.position = new Vector3(dataTransform.position.x, dataTransform.position.y, upBound);
        }
    }

    public override void Select()
    {
        base.Select();
        
        Component component = interactionModule.selectedObject.GetComponent(typeof(InteractiveObjectData));
        var interactive = (InteractiveObjectData) component;

        data = (MoveObjectData) interactive;
        
        data = interactionModule.selectedObject.GetComponent<MoveObjectData>();
        data.GetComponent<Renderer>().material = data.selectedMaterial;
            
        // Selection feedbacks
        
        data.rb.isKinematic = false;
            
        leftBound = transform.position.x - range;
        rightBound = transform.position.x + range;
        downBound = transform.position.z - range;
        upBound = transform.position.z + range;
        
        isPerformed = true;
    }

    public override void Deselect()
    {
        if (data != null)
        {
            data.GetComponent<Outline>().enabled = false;
            data.GetComponent<Renderer>().material = data.defaultMaterial;
            data.rb.isKinematic = true;
        }
        
        // Deselection feedbacks

        base.Deselect();

    }
    
    public override void Release()
    {
        
    }
}
