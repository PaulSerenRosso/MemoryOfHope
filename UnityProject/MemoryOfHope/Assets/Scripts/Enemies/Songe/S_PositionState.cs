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
    { base.StartState(enemyMachine);
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
        var playerPos = PlayerController.instance.transform.position;

        float distance = enemyMachine.agent.remainingDistance;
        
        if (ConditionState.CheckDistance(initialPos, 
            playerPos, pursuitDistance))
        {
            // Check s'il y a un moyen d'atteindre le joueur
            NavMeshPath path = new NavMeshPath();
            enemyMachine.agent.CalculatePath(playerPos, path);
            
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                S_StateMachine enemy = (S_StateMachine) enemyMachine;
                enemy.SwitchState(enemy.pursuitState);
            }
        }
        if(!float.IsPositiveInfinity(distance) && enemyMachine.agent.pathStatus == NavMeshPathStatus.PathComplete && distance <= .01f)
        {
            S_StateMachine enemy = (S_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.hidingState);
        }
    }
}
