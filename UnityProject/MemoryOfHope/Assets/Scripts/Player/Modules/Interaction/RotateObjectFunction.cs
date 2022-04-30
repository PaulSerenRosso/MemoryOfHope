using UnityEngine;
using UnityEngine.InputSystem;

public class RotateObjectFunction : InteractiveObjectFunction
{
    private RotateObjectData data;
    [SerializeField] private float timer = 0;
    [SerializeField] private float time;

    public override void LinkModule()
    {
        GameManager.instance.inputs.Player.InteractionRotate.performed += InputPressed;
        GameManager.instance.inputs.Player.InteractionRotate.canceled += InputReleased;
        isLinked = true;
    }
    
    private void OnDisable()
    {
        UnlinkModule();
    }

    public override void UnlinkModule()
    {
        if (!isLinked) return;
        GameManager.instance.inputs.Player.InteractionRotate.performed -= InputPressed;
        GameManager.instance.inputs.Player.InteractionRotate.canceled -= InputReleased;
    }
    
    public override void InputPressed(InputAction.CallbackContext ctx)
    {
        inputPressed = true;
        Debug.Log("pressed");
    }
    
    public override void InputReleased(InputAction.CallbackContext ctx)
    {
        inputPressed = false;
        Release();
        Debug.Log("unpressed");

    }

    public override void Execute()
    {
        base.Execute();
        Debug.Log("clic : " + timer);

        if (timer <= 0)
        {
            data.transform.Rotate(0, data.rotationDegree, 0);
            
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
        data.tutorial.SetTutorial();
        interactionModule.line.startColor = interactionModule.interactionColor;
        interactionModule.line.endColor = interactionModule.interactionColor;
        data.GetComponent<Outline>().OutlineColor = interactionModule.interactionColor;
        data.interactiveParticleSystem.Stop();
        data = interactionModule.selectedObject.GetComponent<RotateObjectData>();
            
        // Selection feedbacks
        
        data.rb.isKinematic = false;
    }

    public override void Deselect()
    {
        if (data != null)
        {
            data.tutorial.RemoveTutorial();
            data.GetComponent<Outline>().enabled = false;
            data.GetComponent<Outline>().OutlineColor = Color.white;
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

