using System;
using UnityEngine;

[Serializable]
public class S_PausePursuitState : EnemyState
{
    [Header("Parameters")]
    [SerializeField] private float durationBeforePursuit;
    
    [Header("Fixed variables")]
    [SerializeField] private float timer;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.agent.isStopped = true;
        enemyMachine.material.color = Color.green;
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
