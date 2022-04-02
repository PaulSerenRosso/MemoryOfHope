using UnityEngine;

[System.Serializable]
public class MC_PauseShockWaveState : EnemyState
{  
    [Header("Parameters")]
    [Range(0, 1)] [SerializeField] private float durationBeforeShockWave;

    private float timer;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.agent.isStopped = true;
        timer = 0;
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;
        
        if (ConditionState.Timer(durationBeforeShockWave, timer))
        {
            MC_StateMachine enemy = (MC_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.shockWaveState);
        }
    }
}
