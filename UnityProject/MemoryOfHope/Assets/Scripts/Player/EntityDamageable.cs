using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDamageable : MonoBehaviour
{
    public bool isHitByMelee;
    public bool isHitByLaser;
    public bool isBeingKnockback;

    public string tagForMelee;

    [SerializeField] private bool canBenHitByLaser;
    [SerializeField] private bool canBeHitByMelee;
    [SerializeField] private bool canBeKnockbackByMelee;
    
    /*public override void TakeDamage()
    {
        
    }

    public override void Heal()
    {
        
    }

    public override void Death()
    {
        
    }*/
    
    public virtual void OnTriggerEnter(Collider other)
    {
        // Si touché par attaque de mêlée
        // isHitByMelee = true;
    }

    public virtual void OnTriggerStay(Collider other)
    {
        // Si touché par laser
        // isHitByLaser = true;
    }

    public virtual void OnTriggerExit(Collider other)
    {
        isHitByMelee = false;
        isHitByLaser = false;
    }
    
    
}
