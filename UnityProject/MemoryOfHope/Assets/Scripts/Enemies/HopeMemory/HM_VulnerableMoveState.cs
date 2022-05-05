using System;

[Serializable]
public class HM_VulnerableMoveState : EnemyState
{
    public override void StartState(EnemyMachine enemyMachine)
    {
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        // Si suffisemment proche et que timer d'attaque dépassé : lance aléatoirement charge ou shockwave
        
        // Check si la vie du boss est inférieur au seuil, si oui lance pause puis protectedDefault
    }
}
