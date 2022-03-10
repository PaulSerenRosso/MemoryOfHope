using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayerHand : MonoBehaviour
{
  public float currentDamage;
    [SerializeField]
    private Collider collider; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Glitch"))
        {
            AddGlitch();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
    
    public void AddGlitch() // Pour tester
    {
        PlayerManager.instance.hasGlitch = true; 
    }
}
