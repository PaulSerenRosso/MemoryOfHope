using System;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class S_PursuitState : EnemyState
{
    [Header("Parameters")]
    [Range(1, 5)] [SerializeField] private float speed;
    
    private float pursuitDistance;
    private Vector3 initialPos;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        S_StateMachine enemy = (S_StateMachine) enemyMachine;
        pursuitDistance = enemy.pursuitDistance;
        initialPos = enemy.initialPosition;
        enemyMachine.agent.isStopped = false;
        enemyMachine.agent.speed = speed;
        enemyMachine.enemyManager.Animator.SetBool("IsMove", true);
    }
    
    public override void UpdateState(EnemyMachine enemyMachine)
    {
        enemyMachine.agent.SetDestination(PlayerController.instance.transform.position);

        if (!ConditionState.CheckDistance(initialPos, 
            PlayerController.instance.transform.position, pursuitDistance))
        {
            S_StateMachine enemy = (S_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.endPursuitState);
        }
        Debug.Log(enemyMachine.agent.pathStatus.ToString());
        if (enemyMachine.agent.pathStatus != NavMeshPathStatus.PathComplete)
        {
            S_StateMachine enemy = (S_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.pausePositionState);
        }
    }
    
    
    public override void OnCollisionStayState(EnemyMachine enemyMachine, Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            S_StateMachine enemy = (S_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.pauseHitState);
        }
    }
}