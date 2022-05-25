using System;
using UnityEngine;

[Serializable]
public class S_HidingState : EnemyState
{
    [Header("Parameters")] [Range(0, 1)] [SerializeField]
    private float durationBeforeHiding;

    [SerializeField] private Collider[] _colliders;
    [SerializeField] private MeshRenderer _attackMesh;
    private float timer;

    public override void StartState(EnemyMachine enemyMachine)
    {
        base.StartState(enemyMachine);

        enemyMachine.enemyManager.Animator.Play("Despawn");
        enemyMachine.agent.isStopped = true;
        enemyMachine.attackArea.SetActive(true);

        timer = 0;
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        timer += Time.deltaTime;

        if (ConditionState.Timer(durationBeforeHiding, timer))
        {
            for (int i = 0; i < _colliders.Length; i++)
            {
                _colliders[i].enabled = false;
            }

            _attackMesh.enabled = false;
            S_StateMachine enemy = (S_StateMachine) enemyMachine;
            enemy.SwitchState(enemy.defaultState);
        }
    }
}