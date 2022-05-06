using System;

[Serializable]
public class HM_ProtectionProtectedState : EnemyState
{
    public override void StartState(EnemyMachine enemyMachine)
    {
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        // Check si les tours sont détruites, si oui passe en pause transition avec vulnerable default
    }
}
