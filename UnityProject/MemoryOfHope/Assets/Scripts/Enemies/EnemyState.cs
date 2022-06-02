using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public abstract class EnemyState
{
    [SerializeField]
    private UnityEvent _startState;
    public virtual void StartState(EnemyMachine enemyMachine)
    {
        _startState?.Invoke();
    }

    public virtual void CancelHit(EnemyMachine enemyMachine)
    {
        
    }

    public virtual void UpdateState(EnemyMachine enemyMachine)
    {
        
    }

    public virtual void FixedUpdateState(EnemyMachine enemyMachine)
    {
        
    }
    
    public virtual void OnTriggerEnterState(EnemyMachine enemyMachine, Collider other)
    {
        
    }
    
    public virtual void OnTriggerStayState(EnemyMachine enemyMachine, Collider other)
    {
        
    }
    
    public virtual void OnTriggerExitState(EnemyMachine enemyMachine, Collider other)
    {
        
    }
    
    public virtual void OnCollisionEnterState(EnemyMachine enemyMachine, Collision other)
    {
        
    }
    
    public virtual void OnCollisionStayState(EnemyMachine enemyMachine, Collision other)
    {
        
    }
    
    public virtual void OnCollisionExitState(EnemyMachine enemyMachine, Collision other)
    {
        
    }
}
