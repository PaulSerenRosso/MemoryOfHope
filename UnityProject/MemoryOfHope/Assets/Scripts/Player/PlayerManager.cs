using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class PlayerManager : MonoBehaviour, Damageable
{
    #region Variables

    #region Base

    [Header("Base")] [SerializeField] private ShieldManager _shield;
    public List<Module> obtainedModule;
    public int money;
    public bool hasGlitch;
    public bool isActive = true;
    public Checkpoint CurrentCheckpoint;
    public ListenerActivate CurrentListenerActivate;

    #endregion

    #region Health

    [Header("Health")] [SerializeField] public int defaultMaxHealthPlayer;
    public int healthPlayer;
    public int maxHealthPlayer;

    public int maxHealth
    {
        get => maxHealthPlayer;
        set => maxHealthPlayer = value;
    }

    public int health
    {
        get => healthPlayer;
        set => healthPlayer = value;
    }

    #endregion

    #region Death

    [Header("Death")] public bool isDeadPlayer;

    public bool isDead
    {
        get => isDeadPlayer;
        set => isDeadPlayer = value;
    }

    [SerializeField] private float _timeDeath;
    [SerializeField] private float _timeRespawn;
    [SerializeField] private UnityEvent _deathEvent;
    [SerializeField] public UnityEvent _respawnEvent;

    #endregion

    #region TakeDamage

    [Header("TakeDamage")] public Vector3 hitDirection;
    [SerializeField] private float knockbackStrength;
    [SerializeField] private float _blockedKnockbackStrength;
    [SerializeField] private float hitDuration;
    public bool isHit = false;
    [SerializeField] private float _drag;
    [SerializeField] private float _blockedDrag;
    public bool isBlocked;
    [SerializeField] float blockedDuration;

    #endregion

    #region Other

    [Header("Other")] [SerializeField] private float timeNotificationMaxHeart;

    #endregion


    #region Instance

    public static PlayerManager instance;

    private void Awake()
    {
        if (instance is { })
        {
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;
    }

    #endregion

    #endregion


    #region Main Functions

    private void Start()
    {
        for (int i = 0; i < obtainedModule.Count; i++)
        {
            Module module = obtainedModule[i];
            module.LinkModule();
            if (module.isFixedUpdate) PlayerController.instance.activeModulesFixed.Add(module);
            else PlayerController.instance.activeModulesUpdate.Add(module);
        }

        maxHealth = defaultMaxHealthPlayer;
    }

    #region TakeDamage

    public IEnumerator Hit(EnemyManager enemy)
    {
        yield return new WaitForFixedUpdate();
        if (enemy.isBlocked)
        {
            isBlocked = true;
            _shield.TakeDamage(1);
            KnockBack(enemy, _blockedDrag, _blockedKnockbackStrength);
            yield return new WaitForSeconds(blockedDuration);
            isBlocked = false;
            PlayerController.instance.playerRb.drag = 0;
            PlayerController.instance.playerRb.velocity = Vector3.zero;
            yield break;
        }

        isHit = true;
        KnockBack(enemy, _drag, knockbackStrength);
        int damage = enemy.damage;
        Debug.Log($"{enemy.name} a infligé {damage} dégâts");
        TakeDamage(damage);
        yield return new WaitForSeconds(hitDuration);
        PlayerController.instance.playerRb.drag = 0;
        PlayerController.instance.playerRb.velocity = Vector3.zero;

        isHit = false;
    }

    void KnockBack(EnemyManager enemy, float drag, float strengh)
    {
        PlayerController.instance.playerRb.velocity = Vector3.zero;

        Vector3 knockback = new Vector3(hitDirection.x, 0, hitDirection.z);
        knockback.Normalize();
        knockback *= strengh * enemy.Machine.PlayerKnockBackFactor;

        Debug.DrawRay(transform.position, knockback, Color.yellow, 1f);

        PlayerController.instance.currentVelocity += knockback;
        PlayerController.instance.playerRb.drag = drag;
    }

    public void TakeDamage(int damages)
    {
        if (isDead)
            return;
        health -= damages;
        StartCoroutine(Feedbacks.instance.VignetteFeedbacks(.5f, Color.red));
        if (health <= 0)
        {
            health = 0;
            Death();
        }

        if (UIInstance.instance != null)
            UIInstance.instance.DisplayLife();
    }

    public void Heal(int heal)
    {
        health += heal;
        if (UIInstance.instance != null)
            UIInstance.instance.DisplayLife();
    }

    #endregion

    #region Death

    public void Death()
    {
        StartCoroutine(DeathTime());
    }

    IEnumerator DeathTime()
    {
        isDead = true;
        isActive = false;
        _deathEvent?.Invoke();
        yield return new WaitForSeconds(_timeDeath);
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        transform.position = CurrentCheckpoint.SpawnPosition.position;
        transform.rotation = CurrentCheckpoint.SpawnPosition.rotation;
        EnemiesManager.Instance.RefreshBaseEnemies();
        _respawnEvent?.Invoke();
        yield return new WaitForSeconds(_timeRespawn);
        isActive = true;
        Heal(maxHealth);
        isDead = false;
    }

    #endregion

    #endregion

    #region Modules

    public void AddModule(Module mod)
    {
        mod.LinkModule();
        if (mod.isFixedUpdate) PlayerController.instance.activeModulesFixed.Add(mod);
        else PlayerController.instance.activeModulesUpdate.Add(mod);
        obtainedModule.Add(mod);
    }

    #endregion

    #region Trigger & Collision

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MaxLifeItem"))
        {
            maxHealth += 1;

            StartCoroutine(UIInstance.instance.SetNotificationTime("Max Health Improved", timeNotificationMaxHeart));
            Destroy(other.gameObject);
            return;
        }

        if (other.CompareTag("Enemy") && !isHit && !isBlocked)
        {
            CheckEnemyTrigger(other);
        }

        CheckEventTriggerEnter(other);
    }


    private void CheckShockWaveTrigger(Collider other, EnemyManager enemy)
    {
        MC_StateMachine machine = other.GetComponentInParent<MC_StateMachine>();
        var sphere = (SphereCollider) other;

        float distance = Vector3.Magnitude(other.transform.position - transform.position);

        if (distance > sphere.radius - machine.attackAreaLength)
        {
            // Le joueur est dans la zone d'impact
            Debug.Log("Player in area");

            if (transform.position.y < machine.attackArea.transform.position.y + machine.attackAreaHeight)
            {
                // Le joueur est à une altitude d'impact
                Debug.Log("Player pas assez haut");

                hitDirection = transform.position - enemy.transform.position;
                Debug.DrawRay(transform.position, hitDirection, Color.magenta, 1);
                StartCoroutine(Hit(enemy));
            }
        }
    }

    private void CheckEnemyTrigger(Collider other)
    {
        EnemyManager enemy = other.GetComponentInParent<EnemyManager>();

        if (enemy.Machine.GetType() == typeof(MC_StateMachine)) // Si l'attaque est une shock wave
        {
            CheckShockWaveTrigger(other, enemy);
        }
        else
        {
            hitDirection = transform.position - enemy.transform.position;
            Debug.DrawRay(transform.position, hitDirection, Color.magenta, 1);
            StartCoroutine(Hit(enemy));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        CheckEventTriggerStay(other);
    }

    private void OnTriggerExit(Collider other)
    {
        CheckEventTriggerExit(other);
    }

    void CheckEventTriggerEnter(Collider other)
    {
        if (other.CompareTag("EventTrigger"))
        {
            other.gameObject.GetComponent<ListenerTrigger>().Raise();
        }
    }

    void CheckEventTriggerStay(Collider other)
    {
        if (other.CompareTag("EventTriggerStay"))
        {
            other.gameObject.GetComponent<ListenerTriggerStay>().Raise();
        }
    }

    void CheckEventTriggerExit(Collider other)
    {
        if (other.CompareTag("EventTriggerStay"))
        {
            other.gameObject.GetComponent<ListenerTriggerStay>().EndRaise();
        }

        if (other.CompareTag("EventTrigger"))
        {
            other.gameObject.GetComponent<ListenerTrigger>().EndRaise();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
    }

    private void OnCollisionStay(Collision other)
    {
    }


    private void OnCollisionExit(Collision other)
    {
    }

    #endregion
}