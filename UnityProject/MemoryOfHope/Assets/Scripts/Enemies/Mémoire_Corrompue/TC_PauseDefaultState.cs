using UnityEngine;

[System.Serializable]
public class TC_PauseDefaultState : EnemyState
{
    [Header("Parameters")] [SerializeField]
    private float pauseApparition;
    [SerializeField] private Animation wallAnim;
    private float timer;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        timer = 0f;
        Debug.Log("bonsoir  tous");
        wallAnim.Play("WallFadeInTower");
        base.StartState(enemyMachine);
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;

        Debug.Log(wallAnim.isPlaying);
        if (ConditionState.Timer(pauseApparition, timer))
        {
            TC_StateMachine machine = (TC_StateMachine) enemyMachine;
            machine.SwitchState(machine.defaultState);
        }
    }
}
