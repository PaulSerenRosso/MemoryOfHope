using UnityEngine;

[System.Serializable]
public class MC_PauseShockWaveState : EnemyState
{  
    [Header("Parameters")]
    [Range(0, 10)] [SerializeField] private float durationBeforeShockWave;
    [SerializeField] private float rotateSpeed;
    
    [Header("Fixed variables")]
    private float timer;
    [SerializeField] private Transform lookAtTransform;
    
    private bool _rightAnimationAttack = false;

    
    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.enemyManager.Animator.SetBool("IsMove", false);

        if (_rightAnimationAttack)
        {
            _rightAnimationAttack = false;
            enemyMachine.enemyManager.Animator.Play("AttackRight");
        }
        else
        {
            enemyMachine.enemyManager.Animator.Play("AttackLeft");
            _rightAnimationAttack = true;
        }
        
        enemyMachine.agent.isStopped = true;
        timer = 0;
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        lookAtTransform.LookAt(PlayerController.instance.transform);
        
        var tr = enemyMachine.transform;
        tr.rotation = Quaternion.Slerp(tr.rotation, 
            lookAtTransform.rotation, Time.deltaTime * rotateSpeed);
        tr.eulerAngles = new Vector3(0, tr.eulerAngles.y, tr.eulerAngles.z);
        
        timer += Time.deltaTime;

        if (ConditionState.Timer(durationBeforeShockWave, timer))
        {
            MC_StateMachine enemy = (MC_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.shockWaveState);
        }
    }
}
