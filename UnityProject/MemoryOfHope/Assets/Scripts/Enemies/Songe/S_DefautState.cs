using System;
using UnityEngine;

[Serializable]
public class S_DefautState : EnemyState
{
    private float detectionDistance;
    private Vector3 initialPos;

    public override void StartState(EnemyMachine enemyMachine)
    {
        S_StateMachine enemy = (S_StateMachine) enemyMachine;
        detectionDistance = enemy.detectionDistance;
        initialPos = enemy.initialPosition;
        enemyMachine.agent.isStopped = true;
        enemyMachine.material.color = new Color(1, 1, 1, .05f);
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        if (ConditionState.CheckDistance(initialPos, 
            PlayerController.instance.transform.position, detectionDistance))
        {
            S_StateMachine enemy = (S_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.apparitionState);
        }
    }
}
