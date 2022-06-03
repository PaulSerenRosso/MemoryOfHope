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

    private int rand = 0; 
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
        rand = Random.Range(0, 2);
                if (rand == 0)
                {
                     enemyMachine.enemyManager.Animator.SetBool("IsAttack1", true);
                }
                else
                {
                    enemyMachine.enemyManager.Animator.SetBool("IsAttack2", true);
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
            if (rand == 0)
            {
                enemyMachine.enemyManager.Animator.SetBool("IsAttack1", false);
            }
            else
            {
                enemyMachine.enemyManager.Animator.SetBool("IsAttack2", false);
            }
            PA_StateMachine enemy = (PA_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.attackState);
        }
    }

    public override void CancelHit(EnemyMachine enemyMachine)
    {
        base.CancelHit(enemyMachine);
     
            enemyMachine.enemyManager.Animator.SetBool("IsAttack1", false);
        
       
            enemyMachine.enemyManager.Animator.SetBool("IsAttack2", false);
        
    
    }
}
