using System;
using UnityEngine;

[Serializable]
public class MC_ShockWaveState : EnemyState
{
    [Header("Parameters")]
    [Range(0, 2)] [SerializeField] private int durationAttack;
    [Range(0, 3)] [SerializeField] private float shockwaveSpeed;

    private bool _rightAnimationAttack = false;
    private float timer;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
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
        enemyMachine.attackArea.SetActive(true);
        enemyMachine.attackArea.GetComponent<Animation>().Play("ShockwaveCorruptedMemory");
        timer = 0;
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;

        if (ConditionState.Timer(durationAttack, timer))
        {
            enemyMachine.attackArea.SetActive(false);
            MC_StateMachine enemy = (MC_StateMachine) enemyMachine;  
            enemyMachine.enemyManager.isBlocked = false;
            enemy.SwitchState(enemy.positionState);
        }
    }
}
