using System;
using UnityEngine;

[Serializable]
public class S_EndPursuitState : EnemyState
{
    [Header("Parameters")]
    [Range(0, 1)] [SerializeField] private int durationEndPursuit;
    [Range(1, 5)] [SerializeField] private float speed;
    
    private float pursuitDistance;
    private Vector3 initialPos;

    private float timer;

    public override void StartState(EnemyMachine enemyMachine)
    {
        S_StateMachine enemy = (S_StateMachine) enemyMachine;
        pursuitDistance = enemy.pursuitDistance;
        initialPos = enemy.initialPosition;
        enemyMachine.agent.isStopped = false;
        enemyMachine.enemyManager.Animator.SetBool("IsMove", true);
        enemyMachine.agent.speed = speed;
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;
        enemyMachine.agent.SetDestination(PlayerController.instance.transform.position);

        if (ConditionState.Timer(durationEndPursuit, timer))
        {
            timer = 0;
            S_StateMachine enemy = (S_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.pausePositionState);
        }
        else if (ConditionState.CheckDistance(initialPos,
            PlayerController.instance.transform.position, pursuitDistance))
        {
            S_StateMachine enemy = (S_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.pursuitState);
        }
    }
}
