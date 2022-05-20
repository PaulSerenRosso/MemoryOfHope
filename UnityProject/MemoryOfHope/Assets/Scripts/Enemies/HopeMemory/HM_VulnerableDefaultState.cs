using System;
using UnityEngine;

[Serializable]
public class HM_VulnerableDefaultState : EnemyState
{
    [Header("Parameters")]
    [Range(0, 1)] [SerializeField] private float defaultDuration;
    
    private float timer;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        BossPhaseManager.instance.SetNextPhase();
        enemyMachine.enemyManager.canBeHitByMelee = true;
        enemyMachine.attackArea.SetActive(false);

        HM_StateMachine enemy = (HM_StateMachine) enemyMachine;

        enemy.chargeArea.SetActive(false);
        enemy.isProtected = false;
        
        enemy.nextLifeThreshold = enemy.enemyManager.health -
                                  ((VulnerablePhaseSO) BossPhaseManager.instance.currentPhase).damageToInflict;
        Debug.Log($"next threshold : {enemy.nextLifeThreshold}");
        
        UIInstance.instance.SetBossLifeGauge(PhaseType.Vulnerable);

    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;
        
        if (ConditionState.Timer(defaultDuration, timer))
        {
            HM_StateMachine enemy = (HM_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.pauseVulnerableMove);
        }
    }
}
