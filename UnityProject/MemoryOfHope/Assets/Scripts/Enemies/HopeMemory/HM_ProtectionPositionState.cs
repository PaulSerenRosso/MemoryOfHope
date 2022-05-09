using System;

[Serializable]
public class HM_ProtectionPositionState : EnemyState
{
    public override void StartState(EnemyMachine enemyMachine)
    {
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        // Tp le boss à sa position initiale
        
        // Check si la position du boss est correcte, si oui : pause puis protected
    }
}
