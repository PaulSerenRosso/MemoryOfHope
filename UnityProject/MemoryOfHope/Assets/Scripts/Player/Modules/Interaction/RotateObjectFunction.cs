using System.Net.Cache;
using UnityEngine;
using UnityEngine.InputSystem;

// S'il y a un objet ciblé dans RotateObjectModule, alors on peut le sélectionner et le rotate
public class RotateObjectFunction : InteractiveObjectFunction
{
    private RotateObjectData data;
    [SerializeField] private float timer = 0;
    [SerializeField] private float time;

    public override void LinkModule()
    {
        PlayerController.instance.playerActions.Player.InteractionMove.performed += context => InputPressed(context);
        PlayerController.instance.playerActions.Player.InteractionMove.canceled += context => InputReleased(context);
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

        if (timer <= 0)
        {
            float factor;
            if (moveVector.x > 0) factor = -1;
            else factor = 1;
        
            data.transform.Rotate(0, factor * data.rotationDegree, 0);
            
            timer = time;
        }

        timer -= Time.deltaTime;
    }
    
    public override void Select()
    {
        base.Select();
        
        Component component = interactionModule.selectedObject.GetComponent(typeof(InteractiveObjectData));
        var interactive = (InteractiveObjectData) component;

        data = (RotateObjectData) interactive;

        data.interactiveParticleSystem.Stop();
        data = interactionModule.selectedObject.GetComponent<RotateObjectData>();
        data.GetComponent<Renderer>().material = data.selectedMaterial;
            
        // Selection feedbacks
        
        data.rb.isKinematic = false;
    }

    public override void Deselect()
    {
        if (data != null)
        {
            data.GetComponent<Outline>().enabled = false;
            data.GetComponent<Renderer>().material = data.defaultMaterial;
            data.rb.isKinematic = true;
            data.interactiveParticleSystem.transform.position = data.transform.position;
            data.interactiveParticleSystem.Play();
        }
        
        // Deselection feedbacks

        base.Deselect();
    }
    
    public override void Release()
    {
        timer = 0;
    }
}

