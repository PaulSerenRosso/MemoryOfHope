using System;
using UnityEngine;

[Serializable]
public class S_PauseHitState : EnemyState // Quand le songe a touché l'ennemi
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
        enemyMachine.agent.enabled = false;
        enemyMachine.enemyManager.Animator.SetBool("IsMove", false);
        Vector3 knockback = enemyMachine.hitDirection;
        knockback.Normalize();
        knockback *= hitFactor;
        knockback /= enemyMachine.enemyWeigth;

        enemyMachine.rb.isKinematic = false;
        enemyMachine.rb.AddForce(knockback);
        enemyMachine.rb.drag = drag;
        timer = 0;
    }
    
    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;
        
        if (ConditionState.Timer(durationBeforePursuit, timer))
        {
            enemyMachine.rb.isKinematic = true;
            enemyMachine.agent.enabled = true;
            enemyMachine.rb.drag = 0;
            S_StateMachine enemy = (S_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.pursuitState);
        }
    }
}
