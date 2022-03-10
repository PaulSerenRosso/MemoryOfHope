using UnityEngine;

[System.Serializable]
public class PA_PausePursuitState : EnemyState
{
    [Header("Parameters")]
    [SerializeField] private float durationBeforePursuit;
    
    [Header("Fixed variables")]
    [SerializeField] private float timer;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.material.color = Color.green;
        timer = 0;
    }
    
    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;
        
        if (ConditionState.Timer(durationBeforePursuit, timer))
        {
            PA_StateMachine enemy = (PA_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.pursuitState);
        }
    }
}
