using UnityEngine;

[System.Serializable]
public class HM_PauseVulnerableAttack : EnemyState
{
    [Header("Parameters")]
    [Range(0, 1)] [SerializeField] private float durationBeforeAttack;
    
    private float timer;
    
    public override void StartState(EnemyMachine enemyMachine)
    { base.StartState(enemyMachine);
        Debug.Log("HM is about to attack !");

        enemyMachine.agent.isStopped = true;
        timer = 0;
    }
    
    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;
        
        if (ConditionState.Timer(durationBeforeAttack, timer))
        {
            HM_StateMachine enemy = (HM_StateMachine) enemyMachine;
            if (Random.Range(0, 2) == 0)
            {
                enemy.SwitchState(enemy.vulnerableShockwaveState);
            }
            else
            {
                enemy.SwitchState(enemy.vulnerableChargeState);
            }
        }
    }
}
