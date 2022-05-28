using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PA_AttackState : EnemyState
{
    [Header("Parameters")]
    [Range(0, 2)] [SerializeField] private float durationAttack;
    
    private float timer;
    
    public override void StartState(EnemyMachine enemyMachine)
    { base.StartState(enemyMachine);
        enemyMachine.agent.isStopped = true;
        enemyMachine.enemyManager.Animator.Play("Attack");
        enemyMachine.attackArea.SetActive(true);
        timer = 0;
    }
    
    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;
        
        if (ConditionState.Timer(durationAttack, timer))
        {
            enemyMachine.attackArea.SetActive(false);
            PA_StateMachine enemy = (PA_StateMachine) enemyMachine;  
         //   enemyMachine.enemyManager.isBlocked = false;
            enemy.SwitchState(enemy.pursuitState);
        }
    }
    
}
