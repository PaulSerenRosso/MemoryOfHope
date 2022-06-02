using UnityEngine;

[System.Serializable]
public class HM_PauseProtectionPosition : EnemyState
{
    [Header("Parameters")]
    [Range(0, 1)] [SerializeField] private float durationBeforePosition;
    
    private float timer;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.enemyManager.Animator.SetBool("IsProtected", true);
        enemyMachine.enemyManager.Animator.SetBool("IsTp", true);
        enemyMachine.enemyManager.Animator.SetBool("EndTp", false);
        base.StartState(enemyMachine);
        enemyMachine.agent.isStopped = true;
        timer = 0;
    }
    
    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;
        
        if (ConditionState.Timer(durationBeforePosition, timer))
        {
            enemyMachine.enemyManager.Animator.SetBool("EndTp", true);
            enemyMachine.enemyManager.Animator.SetBool("IsTp", false);
            HM_StateMachine enemy = (HM_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.protectionPositionState);
        }
    }
}
