using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ProBuilder.MeshOperations;

public class PlayerManager : MonoBehaviour, Damageable
{
    #region Variables

    #region Base

    [Header("Base")] [SerializeField] private ShieldManager _shield;
    public List<Module> obtainedModule;
    public int money;
    public bool hasGlitch;
    private bool _isActive = true;
    bool isColliding;

    public bool IsActive
    {
        get { return _isActive; }
        set
        {
            _isActive = value;
            if (!value)
            {
                PlayerController.instance.CancelAllModules();
            }
        }
    }

    public List<CheckPoint> CheckPointsReached;
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
        /*if (instance is { })
        {
            DestroyImmediate(gameObject);
            return;
        }*/

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
            UIInstance.instance.AddModuleIcon(module);
            UIInstance.instance.AddModuleGUI(module);
        }

        maxHealth = defaultMaxHealthPlayer;
    }

    #region TakeDamage

    public IEnumerator Hit(EnemyManager enemy)
    {
        yield return new WaitForFixedUpdate();
        if (enemy.isBlocked)
        {
            enemy.isBlocked = false;
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
        PlayerController.instance.playerAnimator.Play("Hit");
        KnockBack(enemy, _drag, knockbackStrength);
        int damage = enemy.damage;
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
        if (isDead) return;
        health -= damages;

        if (health <= 0)
        {
            Death();
        }

        if (UIInstance.instance != null)
        {
            UIInstance.instance.DisplayHealth();
        }
    }

    public void Heal(int heal)
    {
        health += heal;
        if (health > maxHealth) health = maxHealth;
        if (UIInstance.instance != null)
        {
            UIInstance.instance.DisplayHealth();
        }
    }

    #endregion

    #region Death

    public void Death()
    {
        health = 0;
        UIInstance.instance.DisplayHealth();
        StartCoroutine(DeathTime());
    }

    IEnumerator DeathTime()
    {
        isDead = true;
        IsActive = false;
        _deathEvent?.Invoke();
        yield return new WaitForSeconds(_timeDeath);
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        int index = 0;
        float maxSquareDistance = 0;
        for (int i = 0; i < CheckPointsReached.Count; i++)
        {
            float currentSquareDistance =
                Vector3.SqrMagnitude(CheckPointsReached[i].SpawnPosition.position - transform.position);
            if (currentSquareDistance >= maxSquareDistance)
            {
                maxSquareDistance = currentSquareDistance;
                index = i;
            }
        }

        transform.position = CheckPointsReached[index].SpawnPosition.position;
        transform.rotation = CheckPointsReached[index].SpawnPosition.rotation;
        EnemiesManager.Instance.RefreshBaseEnemies();
        _respawnEvent?.Invoke();
        Heal(maxHealth);
        yield return new WaitForSeconds(_timeRespawn);
        IsActive = true;
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
        UIInstance.instance.AddModuleIcon(mod);
        UIInstance.instance.AddModuleGUI(mod);
    }

    #endregion

    #region Trigger & Collision

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !isBlocked)
        {
            CheckEnemyTrigger(other);
        }

        CheckEventTriggerEnter(other);
    }

    private void CheckShockWaveTrigger(Collider other, EnemyManager enemy)
    {
        var machine = other.GetComponentInParent<MC_StateMachine>();
        var sphere = (SphereCollider) other;

        var distance = Vector3.Magnitude(other.transform.position - transform.position);

        if (!(distance > sphere.radius - machine.attackAreaLength)) return;
        if (!(transform.position.y < machine.attackArea.transform.position.y + machine.attackAreaHeight)) return;

        var closestPoint =
            Physics.ClosestPoint(transform.position, other, other.transform.position, other.transform.rotation);
        hitDirection = transform.position - closestPoint;

        StartCoroutine(Hit(enemy));
    }

    private void CheckEnemyTrigger(Collider other)
    {
        var enemy = other.GetComponentInParent<EnemyManager>();

        if (enemy.Machine.GetType() == typeof(MC_StateMachine)) // Si l'attaque est une shock wave
        {
            CheckShockWaveTrigger(other, enemy);
            return;
        }

        var closestPoint =
            Physics.ClosestPoint(transform.position, other, other.transform.position, other.transform.rotation);
        //hitDirection = transform.position - closestPoint;

        hitDirection = transform.position - enemy.transform.position;

        StartCoroutine(Hit(enemy));
    }

    private void OnTriggerStay(Collider other)
    {
        CheckEventTriggerStay(other);
        if (other.CompareTag("MaxLifeItem"))
        {
            other.GetComponent<HeartItem>().GetItem();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CheckEventTriggerExit(other);
    }

    void CheckEventTriggerEnter(Collider other)
    {
        if (other.CompareTag("EventTrigger"))
        {
            ListenerTrigger listenerTrigger = other.gameObject.GetComponent<ListenerTrigger>();
            listenerTrigger.Raise();
        }
    }

    void CheckEventTriggerStay(Collider other)
    {
        if (other.CompareTag("EventTriggerStay"))
        {
            ListenerTriggerStay listenerTriggerStay = other.gameObject.GetComponent<ListenerTriggerStay>();

            listenerTriggerStay.Raise();
        }
    }

    void CheckEventTriggerExit(Collider other)
    {
        if (other.CompareTag("EventTriggerStay"))
        {
            ListenerTriggerStay listenerTriggerStay = other.gameObject.GetComponent<ListenerTriggerStay>();

            listenerTriggerStay.EndRaise();
        }

        if (other.CompareTag("EventTrigger"))
        {
            ListenerTrigger listenerTrigger = other.gameObject.GetComponent<ListenerTrigger>();

            listenerTrigger.EndRaise();
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

    IEnumerator EndColliding()
    {
        yield return new WaitForEndOfFrame();

        isColliding = false;
    }

    #endregion
}