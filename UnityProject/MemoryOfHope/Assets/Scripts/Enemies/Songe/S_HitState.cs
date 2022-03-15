using System;
using UnityEngine;

[Serializable]
public class S_HitState : EnemyState // State quand le Songe est attaqué
{
    [Header("Parameters")]
    [Range(0, 1)] [SerializeField] private float durationHitStunned;

    [SerializeField] private float drag;
    
    private float timer;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.agent.enabled = true;
        enemyMachine.agent.isStopped = true;
        
        if (enemyMachine.enemyManager.canBeKnockback)
        {
            enemyMachine.rb.isKinematic = false;
            enemyMachine.agent.enabled = false;
            Vector3 knockback = (-enemyMachine.transform.forward * enemyMachine.attackStrength) / enemyMachine.enemyWeigth;
            enemyMachine.rb.AddForce(knockback);
            enemyMachine.rb.drag = drag;
        }

        enemyMachine.material.color = Color.red;
        timer = 0;
    }
    
    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;
        
        if (ConditionState.Timer(durationHitStunned, timer))
        {
            S_StateMachine enemy = (S_StateMachine) enemyMachine;
            enemyMachine.agent.enabled = true;
            enemyMachine.rb.drag = 0;
            enemyMachine.rb.isKinematic = true;
            enemy.SwitchState(enemy.pursuitState);
        }
    }

}
