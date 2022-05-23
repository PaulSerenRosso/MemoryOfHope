using System;
using UnityEngine;

[Serializable]
public class HM_VulnerableShockwaveState : EnemyState
{
    [Header("Parameters")] [SerializeField]
    private float shockWaveDuration;

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
        // A la fin de la shockwave, lance cooldown puis move
        timer += Time.deltaTime;

        if (ConditionState.Timer(shockWaveDuration, timer))
        {
            enemyMachine.attackArea.SetActive(false);
            HM_StateMachine enemy = (HM_StateMachine) enemyMachine;
            enemyMachine.enemyManager.isBlocked = false;
            enemy.SwitchState(enemy.cooldownState);
        }
    }
}