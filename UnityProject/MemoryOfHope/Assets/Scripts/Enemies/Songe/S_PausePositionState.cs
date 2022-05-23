using System;
using UnityEngine;

[Serializable]
public class S_PausePositionState : EnemyState
{
    [Header("Parameters")]
    [Range(0, 5)] [SerializeField] private float durationBeforePosition;
    
    private float timer;
    
    public override void StartState(EnemyMachine enemyMachine)
    { base.StartState(enemyMachine);
        enemyMachine.enemyManager.Animator.SetBool("IsMove", false);
        enemyMachine.agent.isStopped = true;
        timer = 0;
    }
    
    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;
        
        if (ConditionState.Timer(durationBeforePosition, timer))
        {
            S_StateMachine enemy = (S_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.positionState);
        }
    }
}
