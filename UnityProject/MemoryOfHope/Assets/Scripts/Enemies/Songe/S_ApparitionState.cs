using System;
using UnityEngine;

[Serializable]
public class S_ApparitionState : EnemyState
{
    [Header("Parameters")] [Range(0, 15)] [SerializeField]
    private float durationBeforePursuit;

    [SerializeField] private Collider[] _colliders;
    [SerializeField] private MeshRenderer _attackMesh;
    private float timer;

    public override void StartState(EnemyMachine enemyMachine)
    {
        base.StartState(enemyMachine);
        enemyMachine.enemyManager.Animator.SetBool("IsSpawn", true);
        enemyMachine.agent.isStopped = true;
        timer = 0;
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;

        if (ConditionState.Timer(durationBeforePursuit, timer))
        {
            S_StateMachine enemy = (S_StateMachine) enemyMachine;
            enemyMachine.enemyManager.Animator.SetBool("IsSpawn", false);

            for (int i = 0; i < _colliders.Length; i++)
            {
                _colliders[i].enabled = true;
            }

            _attackMesh.enabled = true;
            enemy.SwitchState(enemy.pausePursuitState);
        }
    }
}