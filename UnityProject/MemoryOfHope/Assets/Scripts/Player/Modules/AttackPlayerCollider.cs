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
        if (other.CompareTag("Glitch") && !PlayerManager.instance.hasGlitch)
        {
            AddGlitch();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    

    public void AddGlitch()
    {
        PlayerManager.instance.hasGlitch = true; 
    }
}
