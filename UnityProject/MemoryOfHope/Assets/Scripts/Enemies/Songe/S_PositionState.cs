using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class S_PositionState : EnemyState
{
    [Header("Parameters")]
    [Range(1, 5)] [SerializeField] private float speed;
    
    private float detectionDistance;
    private Vector3 initialPos;
    

    public override void StartState(EnemyMachine enemyMachine)
    {
        S_StateMachine enemy = (S_StateMachine) enemyMachine;
        detectionDistance = enemy.detectionDistance;
        initialPos = enemy.initialPosition;
        enemyMachine.material.color = Color.gray;

        enemyMachine.agent.isStopped = false;
        enemyMachine.agent.speed = speed;
        enemyMachine.agent.SetDestination(initialPos);

    }
    
    public override void UpdateState(EnemyMachine enemyMachine)
    {
        float distance = enemyMachine.agent.remainingDistance;

        if (ConditionState.CheckDistance(initialPos, 
            PlayerController.instance.transform.position, detectionDistance))
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
