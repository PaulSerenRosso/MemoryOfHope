using System;
using System.Security.AccessControl;
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
        enemyMachine.rb.isKinematic = false;
        
        Vector3 knockback = new Vector3(enemyMachine.hitDirection.x, 0, enemyMachine.hitDirection.z);
        knockback.Normalize();
        knockback *= enemyMachine.attackStrength;
        knockback /= enemyMachine.enemyWeigth;
        Debug.DrawRay(enemyMachine.transform.position, knockback, Color.green, 1f);
        enemyMachine.rb.AddForce(knockback);
        enemyMachine.rb.drag = drag;
        
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
            enemyMachine.rb.isKinematic = true;
            enemyMachine.rb.velocity = Vector3.zero;
            enemy.SwitchState(enemy.pursuitState);
            enemyMachine.attackStrength = 0;
        }
    }

}
