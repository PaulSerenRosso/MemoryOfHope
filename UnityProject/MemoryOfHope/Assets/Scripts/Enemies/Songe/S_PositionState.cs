using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class S_PositionState : EnemyState
{
    [Header("Parameters")]
    [Range(1, 5)] [SerializeField] private float speed;
    
    private float pursuitDistance;
    private Vector3 initialPos;
    

    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.enemyManager.Animator.SetBool("IsMove", true);
        enemyMachine.agent.isStopped = false;
        S_StateMachine enemy = (S_StateMachine) enemyMachine;
        pursuitDistance = enemy.pursuitDistance;
        initialPos = enemy.initialPosition;

        enemyMachine.agent.speed = speed;
        enemyMachine.agent.SetDestination(initialPos);
    }
    
    public override void UpdateState(EnemyMachine enemyMachine)
    {
        float distance = enemyMachine.agent.remainingDistance;
        //enemyMachine.agent.CalculatePath(enemyMachine.transform.position, PlayerController.instance.transform.position);

        if (ConditionState.CheckDistance(initialPos, 
            PlayerController.instance.transform.position, pursuitDistance))
        {
            S_StateMachine enemy = (S_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.pursuitState);
        }
        else if(!float.IsPositiveInfinity(distance) && enemyMachine.agent.pathStatus == NavMeshPathStatus.PathComplete && distance <= .01f)
        {
            S_StateMachine enemy = (S_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.hidingState);
        }
    }
}
