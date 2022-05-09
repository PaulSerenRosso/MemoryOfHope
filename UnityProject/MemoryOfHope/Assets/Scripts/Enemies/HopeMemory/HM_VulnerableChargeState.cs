using System;

[Serializable]
public class HM_VulnerableChargeState : EnemyState
{
    public override void StartState(EnemyMachine enemyMachine)
    {
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        // A la fin de la charge : cooldown puis move
    }
}
