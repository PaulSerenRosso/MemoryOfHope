using System;
using UnityEngine;

[Serializable]
public class HM_VulnerableShockwaveState : EnemyState
{
    [Header("Parameters")] [SerializeField]
    private float shockWaveDuration;

    private float timer;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        Debug.Log("Shock wave attack !");
        enemyMachine.agent.isStopped = true;
        timer = 0;
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        // A la fin de la shockwave, lance cooldown puis move
        timer += Time.deltaTime;
        
        if (ConditionState.Timer(shockWaveDuration, timer))
        {
            HM_StateMachine enemy = (HM_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.cooldownState);
            
        }
    }
}
