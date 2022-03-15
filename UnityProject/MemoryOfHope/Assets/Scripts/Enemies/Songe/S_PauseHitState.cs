using System;
using UnityEngine;

[Serializable]
public class S_PauseHitState : EnemyState
{
    [Header("Parameters")]
    [Range(0, 1)] [SerializeField] private float durationBeforePursuit;
    [SerializeField] private float drag;
    [SerializeField] private float hitFactor;

    private float timer;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.agent.enabled = true;
        enemyMachine.agent.isStopped = true;

        enemyMachine.rb.isKinematic = false;
        enemyMachine.agent.enabled = false;
        Vector3 knockback = (enemyMachine.hitDirection * hitFactor) / enemyMachine.enemyWeigth;
        enemyMachine.rb.AddForce(knockback);
        enemyMachine.rb.drag = drag;

        enemyMachine.material.color = new Color(1, .5f, 0);
        timer = 0;
    }
    
    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;
        
        if (ConditionState.Timer(durationBeforePursuit, timer))
        {
            enemyMachine.agent.enabled = true;
            enemyMachine.rb.drag = 0;
            enemyMachine.rb.isKinematic = true;
            S_StateMachine enemy = (S_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.pursuitState);
        }
    }
}
