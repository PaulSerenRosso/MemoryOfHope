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
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagForMelee))
        {
            isHitByMelee = true;
        }
        // Si touché par attaque de mêlée
        // isHitByMelee = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Laser"))
        {
            isHitByLaser = true;
        }
        // Si touché par laser
        // isHitByLaser = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isHitByMelee = false;
        isHitByLaser = false;
    }
    
    
}
