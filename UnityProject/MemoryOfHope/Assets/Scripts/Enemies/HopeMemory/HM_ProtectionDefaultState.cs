using System;
using UnityEngine;

[Serializable]
public class HM_ProtectionDefaultState : EnemyState
{
    [Header("Parameters")]
    [Range(0, 1)] [SerializeField] private float defaultDuration;
    
    private float timer;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        BossPhaseManager.instance.SetNextPhase();
        
        // Change l'UI de la barre de vie

    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;
        
        if (ConditionState.Timer(defaultDuration, timer))
        {
            HM_StateMachine enemy = (HM_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.pauseProtectionPosition);
        }
    }
}