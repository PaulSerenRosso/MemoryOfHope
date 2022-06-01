using System;
using UnityEngine;

[Serializable]
public class HM_VulnerableHitState : EnemyState
{
    [Header("Parameters")]
    [SerializeField] private float durationHitStunned;

    [SerializeField] private float drag;
    
    private float timer;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        HM_StateMachine enemy = (HM_StateMachine) enemyMachine;
        enemy.chargeArea.SetActive(false);
        enemyMachine.attackArea.SetActive(false);
        enemyMachine.agent.enabled = true;
        enemyMachine.agent.isStopped = true;
        enemyMachine.agent.enabled = false;
        enemyMachine.rb.isKinematic = false;
        base.StartState(enemyMachine);
        Vector3 knockback = new Vector3(enemyMachine.hitDirection.x, 0, enemyMachine.hitDirection.z);
        knockback.Normalize();
        knockback *= enemyMachine.attackStrength;
        knockback /= enemyMachine.enemyWeigth;
        enemyMachine.rb.AddForce(knockback);
        enemyMachine.rb.drag = drag;
        
        timer = 0;
    }
    
    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;
        
        if (ConditionState.Timer(durationHitStunned, timer))
        {
            HM_StateMachine enemy = (HM_StateMachine) enemyMachine;
            enemyMachine.agent.enabled = true;
            enemyMachine.rb.drag = 0;
            enemyMachine.rb.isKinematic = true;
            enemyMachine.rb.velocity = Vector3.zero;
            enemy.SwitchState(enemy.pauseVulnerableMove);
            //enemyMachine.attackStrength = 0;
        }
    }
    
}
