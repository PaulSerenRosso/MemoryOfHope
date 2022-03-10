using UnityEngine;

[System.Serializable]
public class PA_EndPursuitState : EnemyState
{
    [Header("Parameters")]
    [SerializeField] private int durationEndPursuit;
    [SerializeField] private float detectionDistance;
    [SerializeField] private float speed;

    [Header("Fixed variables")]
    [SerializeField] private float timer;

    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.material.color = Color.cyan;
        timer = 0;
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;
        enemyMachine.agent.isStopped = false;
        enemyMachine.agent.SetDestination(PlayerController.instance.transform.position);

        if (ConditionState.Timer(durationEndPursuit, timer))
        {
            PA_StateMachine enemy = (PA_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.defaultState);
        }
        else if (ConditionState.CheckDistance(enemyMachine.transform.position,
            PlayerController.instance.transform.position, detectionDistance))
        {
            PA_StateMachine enemy = (PA_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.pursuitState);
        }

    }
}
