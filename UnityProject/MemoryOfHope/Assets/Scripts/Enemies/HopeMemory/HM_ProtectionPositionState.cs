using System;
using UnityEngine;

[Serializable]
public class HM_ProtectionPositionState : EnemyState
{
    private Vector3 initialPos;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.agent.isStopped = false;
        
        // Tp le boss à sa position initiale

        HM_StateMachine enemy = (HM_StateMachine) enemyMachine;
        enemy.SwitchState(enemy.protectionProtectedState);

    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        
        
    }
}
