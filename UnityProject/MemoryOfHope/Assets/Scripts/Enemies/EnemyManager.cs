using UnityEngine;

public class EnemyManager : EntityDamageable, Damageable
{
    public int health { get; set; }
    public int maxHealth { get; set; }
    public bool isDead { get; set; }

    public EnemyType enemyType;
    
    public enum EnemyType
    {
        AbandonedPrototype,
        Dream,
        CorruptedMemory
    }
    
    public void TakeDamage()
    {
        
    }

    public void Heal()
    {
        
    }

    public void Death()
    {
        
    }
    
    
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerFist"))
        {
            Debug.Log(other.name);
            isHitByMelee = true;
            HitEnemy(this);

        }
        // Si touché par attaque de mêlée
        // isHitByMelee = true;
    }

    public override void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Laser"))
        {
            isHitByLaser = true;
        }
        // Si touché par laser
        // isHitByLaser = true;
    }

    private void HitEnemy(EnemyManager enemyManager)
    {
        switch (enemyManager.enemyType)
        {
            case EnemyType.AbandonedPrototype:
                PA_StateMachine PA_enemy = enemyManager.GetComponent<PA_StateMachine>();
                PA_enemy.OnHit();
                break;
            
            case EnemyType.Dream:
                S_StateMachine S_enemy = enemyManager.GetComponent<S_StateMachine>();
                S_enemy.OnHit();
                break;
            
            case EnemyType.CorruptedMemory:
                
                break;
        }
    }
    
}


