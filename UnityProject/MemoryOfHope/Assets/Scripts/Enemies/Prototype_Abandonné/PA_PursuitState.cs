using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class PA_PursuitState : EnemyState
{
    [Header("Parameters")]
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;

    [SerializeField] private float speed;

    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.material.color = Color.blue;
        enemyMachine.agent.isStopped = false;
    }
    public override void UpdateState(EnemyMachine enemyMachine)
    {
        enemyMachine.agent.SetDestination(PlayerController.instance.transform.position);
        
        if (ConditionState.CheckDistance(enemyMachine.transform.position, 
            PlayerController.instance.transform.position, minDistance))
        {
            PA_StateMachine enemy = (PA_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.pauseAttackState);
        }
        else if (!ConditionState.CheckDistance(enemyMachine.transform.position, 
            PlayerController.instance.transform.position, maxDistance))
        {
            PA_StateMachine enemy = (PA_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.endPursuitState);
        }
    }
}
