using System;
using UnityEngine;

[Serializable]
public class HM_ProtectionHitState : EnemyState
{

    public override void StartState(EnemyMachine enemyMachine)
    { base.StartState(enemyMachine);
        // Feedback de boucliers
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        HM_StateMachine enemy = (HM_StateMachine) enemyMachine;
        enemy.SwitchState(enemy.protectionProtectedState);
    }
}
