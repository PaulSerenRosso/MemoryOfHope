using System;
using UnityEngine;

[Serializable]
public class S_PursuitState : EnemyState
{
    [Header("Parameters")]
    [SerializeField] private float maxDistance;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.material.color = Color.blue;
        enemyMachine.agent.isStopped = false;
    }
    public override void UpdateState(EnemyMachine enemyMachine)
    {
        enemyMachine.agent.SetDestination(PlayerController.instance.transform.position);
        
        if (!ConditionState.CheckDistance(enemyMachine.transform.position, 
            PlayerController.instance.transform.position, maxDistance))
        {
            S_StateMachine enemy = (S_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.endPursuitState);
        }
    }


    public override void OnCollisionEnterState(EnemyMachine enemyMachine, Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            S_StateMachine enemy = (S_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.pauseHitState);
        }
    }
}
