using UnityEngine;
using System;

[Serializable]
public class MC_HitState : EnemyState // State quand le Songe est attaqué
{
    [Header("Parameters")]
    [Range(0, 1)] [SerializeField] private float durationHitStunned;
    
    [SerializeField] private float drag;

    private float timer;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.attackArea.SetActive(false);
        base.StartState(enemyMachine);
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
            //enemyMachine.enemyManager.isBlocked = false; 
            MC_StateMachine enemy = (MC_StateMachine) enemyMachine;
            enemyMachine.agent.enabled = true;
            enemyMachine.rb.drag = 0;
            enemyMachine.rb.isKinematic = true;
            enemyMachine.rb.velocity = Vector3.zero;
            enemy.SwitchState(enemy.positionState);
            
        }
    }
}
