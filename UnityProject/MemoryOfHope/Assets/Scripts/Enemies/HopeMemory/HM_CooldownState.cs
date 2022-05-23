using System;
using UnityEngine;

[Serializable]
public class HM_CooldownState : EnemyState
{
    [Header("Parameters")]
    [Range(0, 1)] [SerializeField] private float cooldownDuration;
    
    private float timer;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        base.StartState(enemyMachine);
        Debug.Log("cooldown après attack...");
        enemyMachine.agent.isStopped = true;
        timer = 0;
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;
        
        if (ConditionState.Timer(cooldownDuration, timer))
        {
            HM_StateMachine enemy = (HM_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.pauseVulnerableMove);
            
        }
    }
}