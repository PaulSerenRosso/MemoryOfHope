using UnityEngine;

[System.Serializable]
public class PA_DefaultState : EnemyState
{  
    [Header("Parameters")]
    [Range(1, 15)] [SerializeField] public float detectionDistance;

    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.enemyManager.Animator.Play("Idle");
        enemyMachine.enemyManager.Animator.SetBool("IsDetect" , false);
        enemyMachine.enemyManager.Animator.SetBool("IsMove", false);
        enemyMachine.agent.isStopped = true;
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        if (ConditionState.CheckDistance(enemyMachine.transform.position, 
            PlayerController.instance.transform.position, detectionDistance))
        {
            enemyMachine.enemyManager.Animator.SetBool("IsDetect" , true);
            PA_StateMachine enemy = (PA_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.pausePursuitState);
        }
    }
}
