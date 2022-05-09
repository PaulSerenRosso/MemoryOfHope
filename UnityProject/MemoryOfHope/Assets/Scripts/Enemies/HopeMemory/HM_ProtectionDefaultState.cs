using System;

[Serializable]
public class HM_ProtectionDefaultState : EnemyState
{
    public override void StartState(EnemyMachine enemyMachine)
    {
        BossPhaseManager.instance.SetNextPhase();
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        // Change l'UI de la barre de vie

        // Lance pause puis position
    }
}