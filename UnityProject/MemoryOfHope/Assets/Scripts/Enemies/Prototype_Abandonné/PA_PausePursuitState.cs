using UnityEngine;

[System.Serializable]
public class PA_PausePursuitState : EnemyState
{
    [Header("Parameters")]
    [Range(0, 1)] [SerializeField] private float durationBeforePursuit;
    
    private float timer;
    
    public override void StartState(EnemyMachine enemyMachine)
    {    base.StartState(enemyMachine);
        enemyMachine.agent.isStopped = true;
        enemyMachine.enemyManager.Animator.SetBool("IsMove", false);
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
