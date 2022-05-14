using System;
using UnityEngine;

[Serializable]
public class HM_VulnerableChargeState : EnemyState
{
    [Header("Parameters")]
    [Range(0, 10)] [SerializeField] private float chargeDuration;
    [SerializeField] private float chargeSpeed;
    
    private float timer;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.agent.isStopped = true;
        timer = 0;
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        // Charge = pdt un certain temps, set destination vers le joueur, puis après fonce tout droit
        // Attention : s'arrête si touche le joueur
        
        timer += Time.deltaTime;
        
        if (ConditionState.Timer(chargeDuration, timer))
        {
            HM_StateMachine enemy = (HM_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.cooldownState);
            
        }
    }

    public override void OnCollisionEnterState(EnemyMachine enemyMachine, Collision other)
    {
        base.OnCollisionEnterState(enemyMachine, other);
        // Si touche le PJ ?
    }
}
