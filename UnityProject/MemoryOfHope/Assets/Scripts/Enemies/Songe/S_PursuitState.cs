using System;
using Newtonsoft.Json.Serialization;
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
    { base.StartState(enemyMachine);
        S_StateMachine enemy = (S_StateMachine) enemyMachine;
        pursuitDistance = enemy.pursuitDistance;
        initialPos = enemy.initialPosition;
        enemyMachine.agent.isStopped = false;
        enemyMachine.agent.speed = speed;
        enemyMachine.enemyManager.Animator.SetBool("IsMove", true);
    }
    
    public override void UpdateState(EnemyMachine enemyMachine)
    {        
        var playerPos = PlayerController.instance.transform.position;

        enemyMachine.agent.SetDestination(playerPos); // Cherche la pos la plus proche

        if (!ConditionState.CheckDistance(initialPos, 
            playerPos, pursuitDistance))
        {
            S_StateMachine enemy = (S_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.endPursuitState);
        }

        var enemyPosXZ = new Vector3(enemyMachine.agent.destination.x, 0, enemyMachine.agent.destination.z);
        var playerPosXZ = new Vector3(playerPos.x, 0, playerPos.z);

        if (enemyPosXZ != playerPosXZ)
        {
            S_StateMachine enemy = (S_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.pausePositionState);
        }
    }
}