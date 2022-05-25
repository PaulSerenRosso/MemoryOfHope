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
    private float baseStoppingDistance;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
       
        Debug.Log("Charge attack !");
        base.StartState(enemyMachine);
        enemyMachine.agent.isStopped = false;
        enemyMachine.agent.stoppingDistance = 0;
        baseSpeed = enemyMachine.agent.speed;
        baseStoppingDistance = enemyMachine.agent.stoppingDistance;
        enemyMachine.agent.speed = chargeSpeed;
        HM_StateMachine enemy = (HM_StateMachine) enemyMachine;
        enemy.chargeArea.SetActive(true);

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
            enemyMachine.enemyManager.Animator.SetBool("EndCharge", true);
            HM_StateMachine enemy = (HM_StateMachine) enemyMachine;
            enemy.agent.speed = baseSpeed;
            enemy.agent.stoppingDistance = baseStoppingDistance;
            enemy.chargeArea.SetActive(false);
            enemy.SwitchState(enemy.cooldownState);
            
        }
    }

    public override void OnTriggerEnterState(EnemyMachine enemyMachine, Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("hits PJ with charge");
            timer = chargeDuration;
        }
    }
}
