using System;
using UnityEngine;

[Serializable]
public class HM_ProtectionPositionState : EnemyState
{
    private Vector3 initialPos;
    
    public override void StartState(EnemyMachine enemyMachine)
    { base.StartState(enemyMachine);
        enemyMachine.agent.isStopped = true;
        enemyMachine.agent.enabled = false;
        enemyMachine.rb.isKinematic = true;
        // Tp le boss à sa position initiale

        HM_StateMachine enemy = (HM_StateMachine) enemyMachine;

        initialPos = enemy.protectedPos;

        enemy.isProtected = true;
        
        enemy.transform.position = initialPos;
        Debug.Log(enemy.transform.position + initialPos);
        
        enemy.SwitchState(enemy.protectionProtectedState);

    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        
        
    }
}
