using System;

[Serializable]
public class HM_VulnerableShockwaveState : EnemyState
{
    public override void StartState(EnemyMachine enemyMachine)
    {
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        // A la fin de la shockwave, lance cooldown puis move
    }
}
