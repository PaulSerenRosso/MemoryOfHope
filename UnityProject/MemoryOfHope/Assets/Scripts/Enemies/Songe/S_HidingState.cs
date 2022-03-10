using System;
using UnityEngine;

[Serializable]
public class S_HidingState : EnemyState
{
    [Header("Parameters")]
    [SerializeField] private float durationBeforeHiding;
    
    [Header("Fixed variables")]
    [SerializeField] private float timer;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.agent.isStopped = true;
        enemyMachine.material.color = Color.magenta;
        timer = 0;
    }
    
    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;
        
        if (ConditionState.Timer(durationBeforeHiding, timer))
        {
            S_StateMachine enemy = (S_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.defaultState);
        }
    }
}
