using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

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
    public bool isBlocked;
    public bool canBeHitByMelee;
    public bool canBeHitByLaser;
    public bool canBeKnockback;

    //ajouter du knockbackforce pour l'ennemy au joueur
    public int damage;
    [SerializeField] private Animation anim;
    [SerializeField] private GameObject deathFeedback;
    public EnemyMachine Machine;

    #endregion
    
    #region Main Functions

    public virtual void TakeDamage(int damages)
    {
        anim.Play("TakeDamage");
        health -= damages;
        if (health <= 0)
        {
            Death();
        }
    }

    public virtual void HitNoDamage()
    {
        
    }

    public virtual void Heal(int heal)
    {
        
    }

    public virtual void Death()
    {
        for (int i = 0; i < 20; i++)
        {
            Destroy(Instantiate(deathFeedback, transform.position, quaternion.identity),
                Random.Range(2.0f, 3.0f));
        }

        isDead = true;
        Destroy(gameObject);
    }

    #endregion
}

