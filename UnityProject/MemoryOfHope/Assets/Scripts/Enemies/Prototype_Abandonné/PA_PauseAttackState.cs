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
    {
        enemyMachine.agent.isStopped = true;
        timer = 0;
    }
    
    public override void UpdateState(EnemyMachine enemyMachine)
    {
        RaycastHit hit;
        Debug.DrawRay(enemyMachine.transform.position, enemyMachine.transform.forward * 10, Color.green);
        if (Physics.Raycast(enemyMachine.transform.position, enemyMachine.transform.forward, 
            out hit, 10, playerLayers))
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
            lookAtTransform.LookAt(PlayerController.instance.transform);

            var tr = enemyMachine.transform;
            tr.rotation = Quaternion.Slerp(tr.rotation, 
                lookAtTransform.rotation, Time.deltaTime * rotateSpeed);
            tr.eulerAngles = new Vector3(0, tr.eulerAngles.y, tr.eulerAngles.z);
        }
    }
}
