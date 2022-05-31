using UnityEngine;
using UnityEngine.InputSystem;

public class RotateObjectFunction : InteractiveObjectFunction
{
    private RotateObjectData data;
    [SerializeField] private float timer = 0;
    [SerializeField] private float time;
    [SerializeField] private AudioClip rotateSound;

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
    }

    public override void InputReleased(InputAction.CallbackContext ctx)
    {
        inputPressed = false;
        Release();
    }

    public override void Execute()
    {
        base.Execute();

        if (timer <= 0)
        {
            data.transform.Rotate(0, data.rotationDegree, 0);
            data.AudioSource.PlayOneShot(rotateSound);
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

        foreach (var r in data.renderer)
        {
            var mats = r.materials;
            for (int i = 0; i < mats.Length; i++)
            {
                mats[i] = data.selectedMaterial;
            }

            r.materials = mats;
        }

        data.tutorial.SetTutorial();

        data.GetComponent<Outline>().enabled = true;


        //data.GetComponent<Outline>().OutlineColor = interactionModule.interactionColor;
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

            //data.GetComponent<Outline>().OutlineColor = interactionModule.defaultColor;
            
            data.rb.isKinematic = true;
            data.interactiveParticleSystem.transform.position = data.transform.position;
            data.interactiveParticleSystem.Play();
            foreach (var r in data.renderer)
            {
                var mats = r.materials;
                for (int i = 0; i < mats.Length; i++)
                {
                    mats[i] = data.defaultMaterial;
                }

                r.materials = mats;
            }
        }

        // Deselection feedbacks

        base.Deselect();
    }

    public override void Release()
    {
        timer = 0;
    }
}