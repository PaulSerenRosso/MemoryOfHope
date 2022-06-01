using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MoveObjectFunction : InteractiveObjectFunction
{
    private MoveObjectData data;
    [SerializeField] private float range;
    float leftBound;
    float rightBound;
    float downBound;
    float upBound;

    [SerializeField] UnityEvent _feedbackMoveObject;

    public override void LinkModule()
    {
        GameManager.instance.inputs.Player.Move.performed += InputPressed;
        GameManager.instance.inputs.Player.Move.canceled += InputReleased;
        isLinked = true;
    }

    private void OnDisable()
    {
        UnlinkModule();
    }

    public override void UnlinkModule()
    {
        if (!isLinked) return;
        GameManager.instance.inputs.Player.Move.performed -= InputPressed;
        GameManager.instance.inputs.Player.Move.canceled -= InputReleased;
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
        
        if (!data.AudioSource.isPlaying)
        {
            data.AudioSource.Play();
        }

        Vector2 _cameraForwardXZ;
        Vector2 _cameraRightXZ;
        _cameraForwardXZ = new Vector3(MainCameraController.Instance.transform.forward.x,
            MainCameraController.Instance.transform.forward.z).normalized;
        _cameraRightXZ = new Vector3(MainCameraController.Instance.transform.right.x,
            MainCameraController.Instance.transform.right.z).normalized;
        inputCam = _cameraForwardXZ * joystickDirection.y +
                   _cameraRightXZ * joystickDirection.x;
        moveVector = new Vector3(inputCam.x, 0, inputCam.y);

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
        
        

        foreach (var r in data.renderer)
        {
            r.material = data.selectedMaterial;
        }

        data.tutorial.SetTutorial();

        data.GetComponent<Outline>().enabled = false;
        
        data.interactiveParticleSystem.Stop();
        data = interactionModule.selectedObject.GetComponent<MoveObjectData>();

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
            data.tutorial.RemoveTutorial();

            data.AudioSource.Stop();

            //data.GetComponent<Outline>().OutlineColor = interactionModule.defaultColor;
            //data.GetComponent<Outline>().enabled = false;
            data.rb.isKinematic = true;
            data.interactiveParticleSystem.transform.position = data.transform.position;
            data.interactiveParticleSystem.Play();

            foreach (var r in data.renderer)
            {
                r.material = data.defaultMaterial;
            }
        }

        // Deselection feedbacks

        base.Deselect();
    }

    public override void Release()
    {
    }
}