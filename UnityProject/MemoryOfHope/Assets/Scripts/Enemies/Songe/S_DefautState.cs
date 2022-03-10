using System;
using UnityEngine;

[Serializable]
public class S_DefautState : EnemyState
{
    [Header("Parameters")]
    [SerializeField] private float detectionDistance;

    public override void StartState(EnemyMachine enemyMachine)
    {
        enemyMachine.agent.isStopped = true;
        enemyMachine.material.color = Color.white;
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        Debug.Log("Default state !");

        if (ConditionState.CheckDistance(enemyMachine.transform.position, 
            PlayerController.instance.transform.position, detectionDistance))
        {
            S_StateMachine enemy = (S_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.apparitionState);
        }
    }
}
