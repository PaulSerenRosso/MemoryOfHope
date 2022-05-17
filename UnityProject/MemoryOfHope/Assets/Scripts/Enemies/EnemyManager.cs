using System.Collections;
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

    [SerializeField] private float _timeDeath;

    [SerializeField] private float _timerDeath;
    public Animator Animator;
    public int healthEnemy;
    public int maxHealthEnemy;
    public bool isDeadEnemy;
    public bool isBlocked;
    public bool canBeHitByMelee;
    public bool canBeHitByLaser;
    public bool canBeKnockback;
    public Vector3 SpawnPosition;
    public Quaternion SpawnRotation;
    public bool IsBaseEnemy = true;


    //ajouter du knockbackforce pour l'ennemy au joueur
    public int damage;
    [SerializeField] private GameObject deathFeedback;
    public EnemyMachine Machine;
    public ListenerWaveEnemy WaveListener;

    #endregion

    #region Main Functions

    void Start()
    {
        if (IsBaseEnemy)
        {
            SpawnRotation = transform.rotation;
            SpawnPosition = transform.position;
            EnemiesManager.Instance.BaseEnemies.Add(this);
        }
    }

    public void TakeDamage(int damages)
    {
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

        if (WaveListener != null)
            WaveListener.Raise(this);
        if (Animator != null)
            Animator.Play("Death");
        if (Machine.agent != null && Machine.agent.enabled)
            Machine.agent.isStopped = false;
        Machine.enabled = false;
        StartCoroutine(WaitForDeath());
    }

    IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(_timeDeath);
        gameObject.SetActive(false);
    }

    #endregion
}