using UnityEngine;

[System.Serializable]
public class MC_PauseShockWaveState : EnemyState
{  
    [Header("Parameters")]
    [Range(0, 1)] [SerializeField] private float durationBeforeShockWave;
    [SerializeField] private float rotateSpeed;

    private float timer;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.agent.isStopped = true;
        timer = 0;
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        Vector3 targetDir = PlayerController.instance.transform.position - enemyMachine.transform.position;
        
        if (Vector3.Angle(targetDir, enemyMachine.transform.forward) < 60)
        {
            timer += Time.deltaTime;

            if (ConditionState.Timer(durationBeforeShockWave, timer))
            {
                MC_StateMachine enemy = (MC_StateMachine) enemyMachine;
                enemy.SwitchState(enemy.shockWaveState);
            }
        }
        else
        {
            enemyMachine.transform.eulerAngles += new Vector3(0, rotateSpeed, 0) * Time.deltaTime;
        }
        
        
    }
}
