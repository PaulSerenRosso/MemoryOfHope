using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayerCollider : MonoBehaviour
{ 
    public float currentDamage;
    public Collider collider;
    
    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
