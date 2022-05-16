using System;
using UnityEngine;
using System.Collections.Generic;


[Serializable]
public class MC_PositionState : EnemyState
{
    [Header("Parameters")]
    [Range(1, 15)] [SerializeField] private float minDistance;
    [Range(10, 25)] [SerializeField] private float maxDistanceFromTower;

    public List<Vector3> towersPositions = new List<Vector3>();

    public override void StartState(EnemyMachine enemyMachine)
    {
        towersPositions.Clear();
        enemyMachine.enemyManager.Animator.SetBool("IsMove", true);
        // On stock la position des tours corrompues associées à l'ennemi
        MC_StateMachine enemy = (MC_StateMachine) enemyMachine;
        foreach (var tower in enemy.corruptedTowers)
        {
            if(tower == null) continue;
            if(tower.isDeadEnemy) continue;
            towersPositions.Add(tower.transform.position);
        }

        enemyMachine.agent.stoppingDistance = minDistance - 1;
        enemyMachine.agent.isStopped = false;
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        if (towersPositions.Count == 0) // Pas de tour
        {
            enemyMachine.agent.SetDestination(PlayerController.instance.transform.position);
            
            if (ConditionState.CheckDistance(enemyMachine.transform.position, 
                PlayerController.instance.transform.position, minDistance)) // Si le joueur est trop proche : attaque
            {
                MC_StateMachine enemy = (MC_StateMachine) enemyMachine;
                enemy.SwitchState(enemy.pauseShockWaveState);
            }
            else if (!ConditionState.CheckDistance(enemyMachine.transform.position,
                PlayerController.instance.transform.position, maxDistanceFromTower)) // Si la MC est trop loin du joueur
            {
                MC_StateMachine enemy = (MC_StateMachine) enemyMachine;
                enemy.SwitchState(enemy.defaultState);
            }
        }
        else
        {
            foreach (var pos in towersPositions)
            {
                if (ConditionState.CheckDistance(enemyMachine.transform.position, 
                    PlayerController.instance.transform.position, minDistance)) // Si le joueur est trop proche : attaque
                {
                    MC_StateMachine enemy = (MC_StateMachine) enemyMachine;
                    enemy.SwitchState(enemy.pauseShockWaveState);
                }
                else if (!ConditionState.CheckDistance(pos,
                    PlayerController.instance.transform.position, maxDistanceFromTower)) // Si la MC est trop loin d'une tour
                {
                    MC_StateMachine enemy = (MC_StateMachine) enemyMachine;
                    enemy.SwitchState(enemy.defaultState);
                }
                else if (ConditionState.CheckDistance(pos,
                    PlayerController.instance.transform.position, maxDistanceFromTower))
                {
                    enemyMachine.agent.SetDestination(PlayerController.instance.transform.position);
                }
            }
        }
    }
}