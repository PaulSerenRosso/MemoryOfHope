using System;

[Serializable]
public class HM_ProtectionProtectedState : EnemyState
{
    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.agent.isStopped = true;
        enemyMachine.attackArea.SetActive(true);
        enemyMachine.enemyManager.canBeHitByMelee = false;
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        // Check si les tours sont détruites, si oui passe en pause transition avec vulnerable default
    }
}
