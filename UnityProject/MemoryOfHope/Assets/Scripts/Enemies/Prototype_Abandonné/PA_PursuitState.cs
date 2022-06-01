using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class PA_PursuitState : EnemyState
{
    [Header("Parameters")]
    [Range(1, 15)] [SerializeField] private float minDistance;
    [Range(1, 15)] [SerializeField] private float maxDistance;
    
    [SerializeField]
    private float rotationSpeed;
    public override void StartState(EnemyMachine enemyMachine)
    { base.StartState(enemyMachine);
        enemyMachine.enemyManager.Animator.SetBool("IsMove", true);
        enemyMachine.agent.isStopped = false;
        enemyMachine.attackArea.SetActive(false);
    }
    public override void UpdateState(EnemyMachine enemyMachine)
    {
        enemyMachine.agent.SetDestination(PlayerController.instance.transform.position);
        Vector3 vectDist = PlayerController.instance.transform.position - enemyMachine.transform.position;
        Vector3 dirFromAtoB = (vectDist).normalized;
        if (vectDist.magnitude < 2f)
        {
              enemyMachine.transform.forward = Vector3.RotateTowards(enemyMachine.transform.forward, dirFromAtoB, rotationSpeed*Time.deltaTime, 00f);
        }
     
        if (ConditionState.CheckDistance(enemyMachine.transform.position, 
            PlayerController.instance.transform.position, minDistance))
        {
          
            float dotProd = Vector3.Dot(dirFromAtoB, enemyMachine.transform.forward);
            if (dotProd >= 0.8f)
            {
                   PA_StateMachine enemy = (PA_StateMachine) enemyMachine;
                            enemy.SwitchState(enemy.pauseAttackState);
            }
        }
        else if (!ConditionState.CheckDistance(enemyMachine.transform.position, 
            PlayerController.instance.transform.position, maxDistance))
        {
            PA_StateMachine enemy = (PA_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.endPursuitState);
        }
    }
}
