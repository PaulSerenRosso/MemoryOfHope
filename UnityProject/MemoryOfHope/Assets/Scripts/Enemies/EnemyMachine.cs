using UnityEngine;
using UnityEngine.AI;

public class EnemyMachine : MonoBehaviour
{
    #region Main Variables

    public EnemyManager enemyManager;
    
    public EnemyState currentState;
    public Material material;

    public NavMeshAgent agent;
    public Rigidbody rb;
    public float enemyWeigth;
    public float attackStrength;
    public GameObject attackArea;
    public Vector3 hitDirection;

    #endregion

    #region State Machine Main Functions

    public virtual void Start()
    {
        currentState.StartState(this);
    }
    
    public virtual void Update()
    {
        currentState.UpdateState(this);
    }

    public void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    public void SwitchState(EnemyState state)
    {
        rb.velocity = Vector3.zero;
        currentState = state;
        currentState.StartState(this);
    }
    
    public virtual void OnHitByMelee()
    {
        AttackModule attackModule = PlayerController.instance.attackModule;
        PlayerAttackClass attack = attackModule.attackList[attackModule.currentIndexAttack];
        attackStrength = attack.attackStrength;

        if (enemyManager.canBeHitByMelee)
        {
            enemyManager.TakeDamage(attack.damage);
        }
        else
        {
            enemyManager.HitNoDamage();
        }
    }

    #endregion

    public void OnDisable()
    {
        material.color = Color.white;
    }
    
    #region Trigger & Collision
    
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerFist")) // Hit by the player
        {
            hitDirection = transform.position - PlayerController.instance.transform.position;
            Debug.Log(hitDirection);
            hitDirection = -(PlayerController.instance.transform.position - transform.position);
            Debug.Log(hitDirection);

            OnHitByMelee();
        }
    }

    public virtual void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Laser")) // Hit by laser
        {
            
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
