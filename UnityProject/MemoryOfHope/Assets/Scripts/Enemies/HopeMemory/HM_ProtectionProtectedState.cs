using System;
using UnityEditor;

[Serializable]
public class HM_ProtectionProtectedState : EnemyState
{
    private HM_StateMachine machine;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.agent.enabled = true;
        enemyMachine.rb.isKinematic = false;
        enemyMachine.agent.isStopped = true;
        enemyMachine.attackArea.SetActive(true);
        enemyMachine.enemyManager.canBeHitByMelee = false;
        machine = (HM_StateMachine) enemyMachine;
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        foreach (var tower in machine.associatedTowers)
        {
            if (!tower.isDead) return;
        }
        
        machine.SwitchState(machine.vulnerableDefaultState);
    }
}
