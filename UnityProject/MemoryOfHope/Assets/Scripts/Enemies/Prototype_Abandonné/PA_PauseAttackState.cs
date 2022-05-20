using UnityEngine;

[System.Serializable]
public class PA_PauseAttackState : EnemyState
{
    [Header("Parameters")]
    [Range(0, 1)] [SerializeField] private float durationBeforeAttack;
    [SerializeField] private float rotateSpeed;
    
    [Header("Fixed variables")]
    [SerializeField] private LayerMask playerLayers;
    private float timer;
    [SerializeField] private Transform lookAtTransform;

    public override void StartState(EnemyMachine enemyMachine)
    { base.StartState(enemyMachine);
        enemyMachine.enemyManager.Animator.SetBool("IsMove", false);
        enemyMachine.agent.isStopped = true;
        timer = 0;
    }
    
    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;
        
        lookAtTransform.LookAt(PlayerController.instance.transform);
        var tr = enemyMachine.transform;
        tr.rotation = Quaternion.Slerp(tr.rotation, 
            lookAtTransform.rotation, Time.deltaTime * rotateSpeed);
        tr.eulerAngles = new Vector3(0, tr.eulerAngles.y, tr.eulerAngles.z);
        
        if (ConditionState.Timer(durationBeforeAttack, timer))
        {
            PA_StateMachine enemy = (PA_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.attackState);
        }
    }
}
