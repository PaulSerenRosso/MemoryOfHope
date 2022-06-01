using System.Collections;
using UnityEngine;

[System.Serializable]
public class PA_PauseAttackState : EnemyState
{
    [Header("Parameters")]
    [Range(0, 5)] [SerializeField] private float durationBeforeAttack;
    [Range(0, 5)] [SerializeField] private float durationBeforeAnimAttack;
    [SerializeField] private float rotateSpeed;
    
    [Header("Fixed variables")]
    [SerializeField] private LayerMask playerLayers;
    private float timer;
    [SerializeField] private Transform lookAtTransform;

    public override void StartState(EnemyMachine enemyMachine)
    { base.StartState(enemyMachine);
        
        enemyMachine.agent.isStopped = true;
        enemyMachine.enemyManager.Animator.SetBool("IsMove", false);
        timer = 0;
        enemyMachine.StartCoroutine(WaitForAnimationAttack(enemyMachine));


    }

    IEnumerator WaitForAnimationAttack(EnemyMachine enemyMachine)
    {
        yield return new WaitForSeconds(durationBeforeAnimAttack);
                if (Random.Range(0, 2) == 0)
                {
                     enemyMachine.enemyManager.Animator.Play("Attack1");
                }
                else
                {
                    enemyMachine.enemyManager.Animator.Play("Attack2");
                }
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
