using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HM_StateMachine : EnemyMachine
{
    #region States
    
    [Header("Vulnerable State")]
    public HM_VulnerableDefaultState vulnerableDefaultState = new HM_VulnerableDefaultState();
    public HM_VulnerableMoveState vulnerableMoveState = new HM_VulnerableMoveState();
    public HM_VulnerableChargeState vulnerableChargeState = new HM_VulnerableChargeState();
    public HM_CooldownState cooldownState = new HM_CooldownState();
    public HM_VulnerableShockwaveState vulnerableShockwaveState = new HM_VulnerableShockwaveState();
    
    [Header("Protection State")]
    public HM_ProtectionDefaultState protectionDefaultState = new HM_ProtectionDefaultState();
    public HM_ProtectionPositionState protectionPositionState = new HM_ProtectionPositionState();
    public HM_ProtectionProtectedState protectionProtectedState = new HM_ProtectionProtectedState();
    
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
