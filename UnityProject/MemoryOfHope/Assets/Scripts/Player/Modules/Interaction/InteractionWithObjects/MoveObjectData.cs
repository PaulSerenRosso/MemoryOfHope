using System;
using UnityEngine;

public class MoveObjectData : MonoBehaviour
{
    public Material defaultMaterial;
    public Material selectedMaterial;
    public float moveSpeed;
    public Rigidbody rb;
    
    private void Start()
    {
        GetComponent<Renderer>().material = defaultMaterial;
        rb = GetComponent<Rigidbody>();
    }
}
