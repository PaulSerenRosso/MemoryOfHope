using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HM_StateMachine : EnemyMachine
{
    #region States
    
    
    #endregion

    #region State Machine Main Functions

    public void ActivateBehaviour()
    {
        // Active default state
    }

    #endregion

    #region Trigger & Collision

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerFist") && !isHit) // Hit by the player
        {
            hitDirection = transform.position - PlayerController.instance.transform.position;
            hitDirection = -(PlayerController.instance.transform.position - transform.position);

            OnHitByMelee();
        }

        if (other.CompareTag("Shield"))
        {
            enemyManager.isBlocked = true;
        }
    }

    public override void OnTriggerStay(Collider other)
    {
        
    }

    public override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Shield"))
        {
            enemyManager.isBlocked = false;
        }
    }
    
    public override void OnCollisionEnter(Collision other)
    {
        
    }

    public override void OnCollisionStay(Collision other)
    {

        
    }

    public override void OnCollisionExit(Collision other)
    {
        
    }

    #endregion

}
