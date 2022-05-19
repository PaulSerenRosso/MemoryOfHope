using System;
using UnityEngine;

[Serializable]
public class HM_VulnerableChargeState : EnemyState
{
    [Header("Parameters")]
    [Range(0, 10)] [SerializeField] private float chargeDuration;
    [SerializeField] private float chargeSpeed;
    
    private float timer;
    private float baseSpeed;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        Debug.Log("Charge attack !");
        base.StartState(enemyMachine);
        enemyMachine.agent.isStopped = false;
        baseSpeed = enemyMachine.agent.speed;
        enemyMachine.agent.speed = chargeSpeed;
        timer = 0;
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        // Charge = pdt un certain temps, set destination vers le joueur, puis après fonce tout droit
        // Attention : s'arrête si touche le joueur
        enemyMachine.agent.SetDestination(PlayerController.instance.transform.position);
        
        timer += Time.deltaTime;
        
        if (ConditionState.Timer(chargeDuration, timer))
        {
            HM_StateMachine enemy = (HM_StateMachine) enemyMachine;
            enemy.agent.speed = baseSpeed;
            enemy.SwitchState(enemy.cooldownState);
            
        }
    }

    public override void OnCollisionEnterState(EnemyMachine enemyMachine, Collision other)
    {
        base.OnCollisionEnterState(enemyMachine, other);
        // Si touche le PJ ?
    }
}
