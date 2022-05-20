using System;
using UnityEngine;

[Serializable]
public class HM_VulnerableMoveState : EnemyState
{
    [Header("Parameters")] 
    [SerializeField] private float attackDistance;

    [SerializeField] private float durationBeforeAttack;
    private float timer;
    private int nextThreshold;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        Debug.Log("Moving !");
        base.StartState(enemyMachine);
        enemyMachine.agent.isStopped = false;
        timer = 0;
        HM_StateMachine enemy = (HM_StateMachine) enemyMachine;
        nextThreshold = enemy.nextLifeThreshold;
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {

        enemyMachine.agent.SetDestination(PlayerController.instance.transform.position);
        // Si suffisemment proche et que timer d'attaque dépassé : lance pause attack
        
        if (ConditionState.CheckDistance(enemyMachine.transform.position,
            PlayerController.instance.transform.position, attackDistance))
        {
            timer += Time.deltaTime;
            if (ConditionState.Timer(durationBeforeAttack, timer))
            {
                HM_StateMachine enemy = (HM_StateMachine) enemyMachine;
                enemy.SwitchState(enemy.pauseVulnerableAttack);
                
            }
        }
        else
        {
            timer = 0f;
        }

        // Check si la vie du boss est inférieure au seuil, si oui lance pause puis protectedDefault
        if (enemyMachine.enemyManager.health <= nextThreshold)
        {
            HM_StateMachine enemy = (HM_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.protectionDefaultState);
        }
    }
}
