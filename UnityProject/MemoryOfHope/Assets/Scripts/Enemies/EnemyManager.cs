using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
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
    public Animator Animator;
    public int healthEnemy;
    public int maxHealthEnemy;
    public bool isDeadEnemy;
//    public bool isBlocked;
    public bool canBeHitByMelee;
    public bool canBeHitByLaser;
    public bool canBeKnockback;
    public Vector3 SpawnPosition;
    [SerializeField] private UnityEvent _deathEvent;
    [SerializeField] private UnityEvent _damageEvent;
    public Quaternion SpawnRotation;
    public bool IsBaseEnemy = true;


    //ajouter du knockbackforce pour l'ennemy au joueur
    public int damage;
    public EnemyMachine Machine;
    public ListenerWaveEnemy WaveListener ;

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
        if (isDead) return;
        _damageEvent?.Invoke();
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
        health += heal;
        if (health > maxHealth) health = maxHealth;
    }

    public virtual void Death()
    {
        isDead = true;
        _deathEvent?.Invoke();
        if (WaveListener != null)
            WaveListener.Raise(this);
        if (Animator != null)
        {
            StartCoroutine(WaitForLaunchAnimationDeath());
        }

        if (Machine.agent != null && Machine.agent.enabled)
        {
            Machine.agent.isStopped = true;
        }

        Machine.enabled = false;
        StartCoroutine(WaitForDeath());
    }


    IEnumerator WaitForLaunchAnimationDeath()
    {
        yield return new WaitForEndOfFrame();
        Animator.Play("Death");
    }

    IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(_timeDeath);
        gameObject.SetActive(false);
    }

    #endregion
}