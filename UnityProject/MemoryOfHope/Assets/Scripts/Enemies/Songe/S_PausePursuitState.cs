using System;
using UnityEngine;

[Serializable]
public class S_PausePursuitState : EnemyState
{
    [Header("Parameters")] [Range(0, 5)] [SerializeField]
    private float durationBeforePursuit;

    private float timer;

    public override void StartState(EnemyMachine enemyMachine)
    {
        base.StartState(enemyMachine);
        enemyMachine.enemyManager.Animator.SetBool("IsMove", false);
        enemyMachine.agent.isStopped = true;
        enemyMachine.attackArea.SetActive(true);
        timer = 0;
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;

        if (ConditionState.Timer(durationBeforePursuit, timer))
        {
            S_StateMachine enemy = (S_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.pursuitState);
        }
    }
}