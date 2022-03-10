using System;
using UnityEngine;

[Serializable]
public class S_EndPursuitState : EnemyState
{
    [Header("Parameters")]
    [SerializeField] private int durationEndPursuit;
    [SerializeField] private float detectionDistance;

    [Header("Fixed variables")]
    [SerializeField] private float timer;

    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.material.color = Color.cyan;
        timer = 0;
        enemyMachine.agent.isStopped = false;

    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;
        enemyMachine.agent.SetDestination(PlayerController.instance.transform.position);

        if (ConditionState.Timer(durationEndPursuit, timer))
        {
            S_StateMachine enemy = (S_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.pausePositionState);
        }
        else if (ConditionState.CheckDistance(enemyMachine.transform.position,
            PlayerController.instance.transform.position, detectionDistance))
        {
            S_StateMachine enemy = (S_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.pursuitState);
        }
    }
}
