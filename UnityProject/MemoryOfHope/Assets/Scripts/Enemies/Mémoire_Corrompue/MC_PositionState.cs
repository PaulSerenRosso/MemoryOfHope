using System;
using UnityEngine;

[Serializable]
public class MC_PositionState : EnemyState
{
    [Header("Parameters")]
    [Range(1, 15)] [SerializeField] private float minDistance;
    [Range(1, 15)] [SerializeField] private float maxDistance;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.agent.isStopped = false;
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        enemyMachine.agent.SetDestination(PlayerController.instance.transform.position);
        
        if (ConditionState.CheckDistance(enemyMachine.transform.position, 
            PlayerController.instance.transform.position, minDistance))
        {
            MC_StateMachine enemy = (MC_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.pauseShockWaveState);
        }
        else if (!ConditionState.CheckDistance(enemyMachine.transform.position, 
            PlayerController.instance.transform.position, maxDistance))
        {
            MC_StateMachine enemy = (MC_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.defaultState);
        }
    }
}