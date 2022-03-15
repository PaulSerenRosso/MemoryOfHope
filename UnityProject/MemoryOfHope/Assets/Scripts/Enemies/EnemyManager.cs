using UnityEngine;

public class EnemyManager : MonoBehaviour, Damageable
{
    #region Variables

    public int health 
    { 
        get { return healthEnemy; }
        set { healthEnemy = value; }
    }
    public int maxHealth

    {
        get { return maxHealthEnemy; }
        set { maxHealthEnemy = value; }
    }
    public bool isDead
    {
        get => isDeadEnemy;
        set => isDeadEnemy = value;
    }
    public int healthEnemy;
    public int maxHealthEnemy;
    public bool isDeadEnemy;
    
    public bool canBeHitByMelee;
    public bool canBeHitByLaser;
    public bool canBeKnockback;

    public int damage;

    #endregion
    
    #region Main Functions

    public void TakeDamage(int damages)
    {
        health -= damages;

        if (health <= 0)
        {
            Death();
        }
    }

    public void HitNoDamage()
    {
        // 
    }

    public void Heal(int heal)
    {
        
    }

    public void Death()
    {
        //Destroy(GetComponent<NavMeshAgent>());
        Destroy(gameObject);
    }

    #endregion
}

