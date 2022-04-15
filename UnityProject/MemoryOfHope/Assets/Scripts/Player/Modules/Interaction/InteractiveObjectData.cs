using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObjectData : MonoBehaviour
{
    public InteractionType type;
    
    public Material defaultMaterial;
    public Material selectedMaterial;
    public Rigidbody rb;
    public ParticleSystem interactiveParticleSystem;
    
    public virtual void Start()
    {
        GetComponent<Renderer>().material = defaultMaterial;
        rb = GetComponent<Rigidbody>();
    }
}
public enum InteractionType
{
    Move, Rotate
}
