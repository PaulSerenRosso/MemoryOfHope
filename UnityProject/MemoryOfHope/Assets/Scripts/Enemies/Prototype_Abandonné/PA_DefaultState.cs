using UnityEngine;

[System.Serializable]
public class PA_DefaultState : EnemyState
{  
    [Header("Parameters")]
    [SerializeField] private float detectionDistance;

    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.material.color = Color.white;
        
        enemyMachine.agent.isStopped = true;
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        if (ConditionState.CheckDistance(enemyMachine.transform.position, 
            PlayerController.instance.transform.position, detectionDistance))
        {
            PA_StateMachine enemy = (PA_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.pausePursuitState);
        }
    }
}
