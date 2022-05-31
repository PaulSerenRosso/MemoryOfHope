using UnityEngine;

public class InteractiveObjectData : MonoBehaviour
{
    public InteractionType type;
    public AudioSource AudioSource;
    public Rigidbody rb;
    public Renderer[] renderer;
    public Material defaultMaterial;
    public Material selectedMaterial;
    public ParticleSystem interactiveParticleSystem;
    public TutorialGameEvent tutorial;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
}

public enum InteractionType
{
    Move,
    Rotate
}