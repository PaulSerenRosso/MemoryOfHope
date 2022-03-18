using UnityEngine;

[System.Serializable]
public class PA_PauseAttackState : EnemyState
{
    [Header("Parameters")]
    [Range(0, 1)] [SerializeField] private float durationBeforeAttack;
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

            if (ConditionState.Timer(durationBeforeAttack, timer))
            {
                PA_StateMachine enemy = (PA_StateMachine) enemyMachine;
                enemy.SwitchState(enemy.attackState);
            }
        }
        else
        {
            enemyMachine.transform.eulerAngles += new Vector3(0, rotateSpeed, 0) * Time.deltaTime;
        }
    }
}
