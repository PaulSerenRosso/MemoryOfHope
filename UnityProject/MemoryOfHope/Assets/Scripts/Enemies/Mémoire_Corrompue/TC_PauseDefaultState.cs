using UnityEngine;

[System.Serializable]
public class TC_PauseDefaultState : EnemyState
{
    [Header("Parameters")] [SerializeField]
    private float pauseApparition;

    private float timer;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        timer = 0f;
        base.StartState(enemyMachine);
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;

        if (ConditionState.Timer(pauseApparition, timer))
        {
            TC_StateMachine machine = (TC_StateMachine) enemyMachine;
            machine.SwitchState(machine.defaultState);
        }
    }
}
