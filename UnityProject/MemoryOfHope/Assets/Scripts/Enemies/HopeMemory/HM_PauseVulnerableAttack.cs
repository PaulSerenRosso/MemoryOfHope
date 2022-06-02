using UnityEngine;

[System.Serializable]
public class HM_PauseVulnerableAttack : EnemyState
{
    [Header("Parameters")] [SerializeField]
    private float durationBeforeShockWave;

    [SerializeField] private float durationBeforeCharge;

    private int rand;
    private float timer;
    private float currentTime;

    public override void StartState(EnemyMachine enemyMachine)
    {
        base.StartState(enemyMachine);

        rand = Random.Range(0, 2);
        if (rand == 0)
        {
            currentTime = durationBeforeShockWave;
            enemyMachine.enemyManager.Animator.SetBool("IsShock", true);
        }
        else
        {
            enemyMachine.enemyManager.Animator.SetBool("EndCharge", false);
            currentTime = durationBeforeCharge;
            enemyMachine.enemyManager.Animator.SetBool("IsCharge", true);
        }

        enemyMachine.agent.isStopped = true;
        timer = 0;
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;

        if (ConditionState.Timer(currentTime, timer))
        {
            HM_StateMachine enemy = (HM_StateMachine) enemyMachine;
            if (rand == 0)
            {
                enemy.SwitchState(enemy.vulnerableShockwaveState);
                enemyMachine.enemyManager.Animator.SetBool("IsShock",  false);
            }
            else
            {
                enemy.SwitchState(enemy.vulnerableChargeState);
                enemyMachine.enemyManager.Animator.SetBool("IsCharge", false);
            }
        }
    }
}