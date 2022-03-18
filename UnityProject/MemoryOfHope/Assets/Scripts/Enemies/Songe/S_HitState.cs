using System;
using UnityEngine;

[Serializable]
public class S_HitState : EnemyState // State quand le Songe est attaqué
{
    [Header("Parameters")]
    [Range(0, 1)] [SerializeField] private float durationHitStunned;
    
    private float timer;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.agent.enabled = true;
        enemyMachine.agent.isStopped = true;
        timer = 0;
    }
    
    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;
        
        if (ConditionState.Timer(durationHitStunned, timer))
        {
            S_StateMachine enemy = (S_StateMachine) enemyMachine;
            enemyMachine.agent.enabled = true;
            enemy.SwitchState(enemy.pursuitState);
        }
    }
}
