using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PA_AttackState : EnemyState
{
    
    [Header("Parameters")]
    [SerializeField] private int durationAttack;
    
    [Header("Fixed variables")]
    [SerializeField] private float timer;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.agent.isStopped = true;
        enemyMachine.material.color = Color.red;
        timer = 0;

    }
    
    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;
        
        if (ConditionState.Timer(durationAttack, timer))
        {
            PA_StateMachine enemy = (PA_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.pursuitState);
        }
    }
}
