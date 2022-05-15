using UnityEngine;

[System.Serializable]
public class HM_PauseVulnerableMove : EnemyState
{
    [Header("Parameters")]
    [Range(0, 1)] [SerializeField] private float durationBeforePursuit;
    
    private float timer;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        Debug.Log("Pause avant de move");

        enemyMachine.agent.isStopped = true;
        timer = 0;
    }
    
    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;
        
        if (ConditionState.Timer(durationBeforePursuit, timer))
        {
            HM_StateMachine enemy = (HM_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.vulnerableMoveState);
        }
    }
}
