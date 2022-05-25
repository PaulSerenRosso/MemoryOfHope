using System;

[Serializable]
public class HM_ProtectionProtectedState : EnemyState
{
    private HM_StateMachine machine;

    public override void StartState(EnemyMachine enemyMachine)
    {
        base.StartState(enemyMachine);
        enemyMachine.rb.isKinematic = false;
        enemyMachine.agent.enabled = true;
        enemyMachine.agent.isStopped = true;
        enemyMachine.enemyManager.canBeHitByMelee = false;
        machine = (HM_StateMachine) enemyMachine;
        machine.chargeArea.SetActive(true);
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        foreach (var tower in machine.associatedTowers)
        {
            if (!tower.isDead) return;
        }

        enemyMachine.enemyManager.Animator.SetBool("IsProtected", false);
        machine.SwitchState(machine.vulnerableDefaultState);
    }
}