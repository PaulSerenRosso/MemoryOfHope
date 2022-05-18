using UnityEngine;
using UnityEngine.AI;

public class EnemyMachine : MonoBehaviour
{
    #region Main Variables

    public EnemyManager enemyManager;

    protected bool _isCurrentAttackKnockback;
    public EnemyState currentState;

    public float PlayerKnockBackFactor;
    public NavMeshAgent agent;
    public Rigidbody rb;
    public float enemyWeigth;
    public float attackStrength;
    public GameObject attackArea;
    public Vector3 hitDirection;
    public bool isHit = false;

    #endregion

    #region State Machine Main Functions

    public virtual void Start()
    {
        currentState?.StartState(this);
    }

    public virtual void Update()
    {
        currentState?.UpdateState(this);
    }

    public void FixedUpdate()
    {
        currentState?.FixedUpdateState(this);
    }

    public void SwitchState(EnemyState state)
    {
        if (gameObject.activeSelf)
        {
            rb.velocity = Vector3.zero;
            currentState = state;
            currentState.StartState(this);
        }
    }

    public virtual void OnHitByMelee()
    {
        AttackModule attackModule = PlayerController.instance.attackModule;
        PlayerAttackClass attack = attackModule.attackList[attackModule.currentIndexAttack];
        if (attack.IsKnockbackEnemy)
        {
            _isCurrentAttackKnockback = true;
            attackStrength = attack.attackStrength;

            hitDirection = transform.position - PlayerController.instance.transform.position;
            hitDirection = -(PlayerController.instance.transform.position - transform.position);
        }
        else
        {
            _isCurrentAttackKnockback = false;
        }


        if (enemyManager.canBeHitByMelee)
        {
            enemyManager.TakeDamage(attack.damage);
        }
        else
        {
            enemyManager.HitNoDamage();
        }
    }

    public virtual void OnHitByLaser()
    {
        // Inflige damage aux songes
    }

    #endregion

    #region Trigger & Collision

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerFist") && !isHit) // Hit by the player
        {
            Debug.Log("hits by fists");
            if (!enemyManager.isDead)
            {
                OnHitByMelee();
            }
        }
    }

    public virtual void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Laser") && !isHit) // Hit by laser
        {
            if (!enemyManager.isDead)
            {
                OnHitByLaser();
            }
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
    }

    public virtual void OnCollisionEnter(Collision other)
    {
    }

    public virtual void OnCollisionStay(Collision other)
    {
    }

    public virtual void OnCollisionExit(Collision other)
    {
    }

    #endregion
}