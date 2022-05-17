using UnityEngine;

[System.Serializable]
public class MC_PausePositionState : EnemyState
{  
    [Header("Parameters")]
    [Range(0, 1)] [SerializeField] private float durationBeforePosition;

    private float timer;

    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.enemyManager.Animator.SetBool("IsMove", false);
        enemyMachine.agent.isStopped = true;
        timer = 0;
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;
        
        if (ConditionState.Timer(durationBeforePosition, timer))
        {
            MC_StateMachine enemy = (MC_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.positionState);
        }
    }
}
