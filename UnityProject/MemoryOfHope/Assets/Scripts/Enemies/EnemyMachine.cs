using System;
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

    #endregion
    

    #region State Machine Main Functions

    public virtual void Start()
    {
        Debug.Log(this);
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
        currentState = state;
        currentState.StartState(this);
    }
    
    public virtual void OnHitByMelee()
    {
        AttackModule attackModule = PlayerController.instance.attackModule;
        PlayerAttackClass attack = attackModule.attackList[attackModule.currentIndexAttack];
        attackStrength = attack.attackStrength;
        enemyManager.TakeDamage(attack.damage);
    }

    #endregion

    public void OnDisable()
    {
        material.color = Color.white;
    }

    public virtual void OnTriggerEnter(Collider other)
    {

    }

    public virtual void OnTriggerStay(Collider other)
    {

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
}
