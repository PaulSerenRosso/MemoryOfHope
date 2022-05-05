using System;

[Serializable]
public class HM_VulnerableDefaultState : EnemyState
{
    public override void StartState(EnemyMachine enemyMachine)
    {
        BossPhaseManager.instance.SetNextPhase();
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        // Fixe le seuil de pv pour changer de phase
        
        // Passe en pause puis en move
    }
}
