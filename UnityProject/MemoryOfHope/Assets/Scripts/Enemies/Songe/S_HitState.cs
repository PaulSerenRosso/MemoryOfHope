using System;
using UnityEngine;

[Serializable]
public class S_HitState : EnemyState
{
    [Header("Parameters")]
    [Range(0, 1)] [SerializeField] private float durationHitStunned;
    
    private float timer;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.agent.isStopped = true;

        enemyMachine.material.color = Color.red;
        timer = 0;
    }
    
    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;
        
        if (ConditionState.Timer(durationHitStunned, timer))
        {
            S_StateMachine enemy = (S_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.pursuitState);
        }
    }
}
