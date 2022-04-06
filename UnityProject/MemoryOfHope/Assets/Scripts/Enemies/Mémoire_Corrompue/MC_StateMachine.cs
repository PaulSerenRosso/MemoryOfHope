using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_StateMachine : EnemyMachine
{
    public float attackAreaLength;
    public float attackAreaHeight;

    public CorruptedTowerManager[] corruptedTowers;

    public bool isProtected;

    #region States
    
    public MC_DefaultState defaultState = new MC_DefaultState();

    public MC_PositionState positionState = new MC_PositionState();

    public MC_ShockWaveState shockWaveState = new MC_ShockWaveState();

    public MC_HitState hitState = new MC_HitState();

    public MC_PausePositionState pausePositionState = new MC_PausePositionState();

    public MC_PauseShockWaveState pauseShockWaveState = new MC_PauseShockWaveState();

    #endregion

    #region Gizmos

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, defaultState.detectionDistance);
    }

    #endregion

    #region State Machine Main Functions

    public override void Start()
    {
        currentState = defaultState;
        base.Start();
    }
    
    public override void OnHitByMelee()
    {
        base.OnHitByMelee();
        SwitchState(hitState);
    }

    public bool IsProtected()
    {
        foreach (var towers in corruptedTowers)
        {
            if (!towers.isDeadEnemy)
            {
                return true;
            }
        }
        return false;
    }
    
    #endregion

    #region Trigger & Collision

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerFist") && !isHit && !isProtected) // Hit by the player
        {
            hitDirection = transform.position - PlayerController.instance.transform.position;
            hitDirection = -(PlayerController.instance.transform.position - transform.position);

            OnHitByMelee();
        }
        else if (isProtected)
        {
            Debug.Log("Corrupted Memory is protected");
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
