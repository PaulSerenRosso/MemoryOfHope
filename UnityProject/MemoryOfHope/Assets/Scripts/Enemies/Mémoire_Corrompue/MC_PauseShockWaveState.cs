using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class MC_PauseShockWaveState : EnemyState
{  
    [Header("Parameters")]
    [Range(0, 1)] [SerializeField] private float durationBeforeShockWave;
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
        /*
        RaycastHit hit;
        Debug.DrawRay(enemyMachine.transform.position, enemyMachine.transform.forward * 10, Color.green);
        
        if (Physics.Raycast(enemyMachine.transform.position - Vector3.up, enemyMachine.transform.forward, 
            out hit, 10, playerLayers))
        {
            
        }
        else
        {
            lookAtTransform.LookAt(PlayerController.instance.transform);
            enemyMachine.transform.rotation = Quaternion.Slerp(enemyMachine.transform.rotation, 
                lookAtTransform.rotation, Time.deltaTime * rotateSpeed);
        }*/
        
        lookAtTransform.LookAt(PlayerController.instance.transform);
        enemyMachine.transform.rotation = Quaternion.Slerp(enemyMachine.transform.rotation, 
            lookAtTransform.rotation, Time.deltaTime * rotateSpeed);
        
        timer += Time.deltaTime;

        if (ConditionState.Timer(durationBeforeShockWave, timer))
        {
            MC_StateMachine enemy = (MC_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.shockWaveState);
        }
    }
}
