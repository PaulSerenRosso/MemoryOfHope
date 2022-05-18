using UnityEngine;

[System.Serializable]
public class MC_DefaultState : EnemyState
{
    [Header("Parameters")] [Range(1, 15)] [SerializeField]
    public float detectionDistance;


    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.enemyManager.Animator.SetBool("IsMove", false);
        enemyMachine.agent.isStopped = true;
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        if (ConditionState.CheckDistance(enemyMachine.transform.position,
            PlayerController.instance.transform.position, detectionDistance))
        {
            MC_StateMachine enemy = (MC_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.pausePositionState);
        }
    }
}