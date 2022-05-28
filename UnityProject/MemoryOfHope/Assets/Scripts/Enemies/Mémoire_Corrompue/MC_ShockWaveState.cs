using System;
using UnityEngine;

[Serializable]
public class MC_ShockWaveState : EnemyState
{
    [Header("Parameters")] [Range(0, 2)] [SerializeField]
    private int durationAttack;
    [SerializeField] private ParticleSystem shockwave;

    private float timer;

    public override void StartState(EnemyMachine enemyMachine)
    {
        base.StartState(enemyMachine);
        enemyMachine.agent.isStopped = true;
        enemyMachine.attackArea.SetActive(true);
        shockwave.Play();
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
          //  enemyMachine.enemyManager.isBlocked = false;
            enemy.SwitchState(enemy.positionState);
        }
    }
}