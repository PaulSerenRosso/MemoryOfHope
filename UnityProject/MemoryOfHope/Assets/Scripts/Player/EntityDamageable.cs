using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDamageable : MonoBehaviour
{
    public bool isHitByMelee;
    public bool isHitByLaser;
    public bool isBeingKnockback;
    
    [SerializeField] private bool canBenHitByLaser;
    [SerializeField] private bool canBeHitByMelee;
    [SerializeField] private bool canBeKnockbackByMelee;
    
    public virtual void OnTriggerEnter(Collider other)
    {
        
    }

    public virtual void OnTriggerStay(Collider other)
    {
        
    }

    public virtual void OnTriggerExit(Collider other)
    {

    }
}
