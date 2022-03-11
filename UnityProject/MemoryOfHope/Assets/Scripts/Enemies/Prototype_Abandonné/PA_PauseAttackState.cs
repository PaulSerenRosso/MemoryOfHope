using UnityEngine;

[System.Serializable]
public class PA_PauseAttackState : EnemyState
{
    [Header("Parameters")]
    [Range(0, 1)] [SerializeField] private float durationBeforeAttack;
    
    private float timer;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.material.color = Color.yellow;
        enemyMachine.agent.isStopped = true;
        timer = 0;
    }
    
    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;
        
        if (ConditionState.Timer(durationBeforeAttack, timer))
        {
            PA_StateMachine enemy = (PA_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.attackState);
        }
    }
}
