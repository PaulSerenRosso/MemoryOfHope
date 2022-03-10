using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class S_PositionState : EnemyState
{
    [Header("Parameters")]
    [SerializeField] private float detectionDistance;
    [SerializeField] private float hidingDistance;

    [Header("Fixed variables")] 
    private Vector3 pos;

    public override void StartState(EnemyMachine enemyMachine)
    {
        S_StateMachine enemy = (S_StateMachine) enemyMachine;
        pos = enemy.initialPosition;
        enemyMachine.material.color = Color.gray;

        enemyMachine.agent.isStopped = false;
        enemyMachine.agent.SetDestination(pos);

    }
    
    public override void UpdateState(EnemyMachine enemyMachine)
    {
        float distance = enemyMachine.agent.remainingDistance;

        if (ConditionState.CheckDistance(enemyMachine.transform.position, 
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
