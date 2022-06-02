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
        base.StartState(enemyMachine);
        if (_rightAnimationAttack)
        {
            _rightAnimationAttack = false;
            enemyMachine.enemyManager.Animator.SetBool("IsAttackRight", true);
        }
        else
        {
            enemyMachine.enemyManager.Animator.SetBool("IsAttackLeft", true); 
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
            if (!_rightAnimationAttack)
            {
                enemyMachine.enemyManager.Animator.SetBool("IsAttackRight", false);
            }
            else
            {
                enemyMachine.enemyManager.Animator.SetBool("IsAttackLeft", false); 
            }
            MC_StateMachine enemy = (MC_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.shockWaveState);
        }
    }

    public override void CancelHit(EnemyMachine enemyMachine)
    {
        base.CancelHit(enemyMachine);
        if (!_rightAnimationAttack)
        {
            enemyMachine.enemyManager.Animator.SetBool("IsAttackRight", false);
        }
        else
        {
            enemyMachine.enemyManager.Animator.SetBool("IsAttackLeft", false); 
        }
    }
}
