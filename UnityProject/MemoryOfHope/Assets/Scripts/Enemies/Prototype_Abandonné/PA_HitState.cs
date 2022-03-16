using System;
using UnityEngine;

[Serializable]
public class PA_HitState : EnemyState // State quand le Prototype Abandonné est attaqué
{
    [Header("Parameters")]
    [Range(0, 1)] [SerializeField] private float durationHitStunned;

    [SerializeField] private float drag;
    
    private float timer;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.agent.enabled = true;
        enemyMachine.agent.isStopped = true;
        enemyMachine.agent.enabled = false;
        
        Vector3 knockback = enemyMachine.hitDirection * enemyMachine.attackStrength / enemyMachine.enemyWeigth;
        Debug.DrawRay(enemyMachine.transform.position, knockback, Color.green, 1f);
        
        enemyMachine.rb.AddForce(knockback);
        enemyMachine.rb.drag = drag;

        enemyMachine.material.color = Color.red;
        timer = 0;
    }
    
    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;
        
        if (ConditionState.Timer(durationHitStunned, timer))
        {
            PA_StateMachine enemy = (PA_StateMachine) enemyMachine;
            enemyMachine.agent.enabled = true;
            enemyMachine.rb.drag = 0;
            enemy.SwitchState(enemy.pursuitState);
        }
    }

}
