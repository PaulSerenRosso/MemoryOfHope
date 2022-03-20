using UnityEngine;
using UnityEngine.InputSystem;

// S'il y a un objet ciblé dans MoveObjectModule, alors on peut le sélectionner et le déplacer
public class MoveObjectFunction : Module
{
    [SerializeField] private MoveObjectModule moveObjectModule;
    [SerializeField] private MoveObjectData data;
    [SerializeField] private Vector3 joystickDirection;
    private Vector3 moveVector;
    private Vector2 inputCam;
    
    float leftBound;
    float rightBound;
    float downBound;
    float upBound;
    
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

    public void Select()
    {
        isPerformed = true;
        PlayerController.instance.playerRb.isKinematic = true;
        data = moveObjectModule.selectedObject.GetComponent<MoveObjectData>();
        data.GetComponent<Renderer>().material = data.selectedMaterial;
        
        leftBound = transform.position.x - moveObjectModule.rayLength;
        rightBound = transform.position.x + moveObjectModule.rayLength;
        downBound = transform.position.z - moveObjectModule.rayLength;
        upBound = transform.position.z + moveObjectModule.rayLength;
        
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

        PlayerController.instance.playerRb.isKinematic = false;
        isPerformed = false;
    }
    
    public override void Release()
    {
        
    }
}
