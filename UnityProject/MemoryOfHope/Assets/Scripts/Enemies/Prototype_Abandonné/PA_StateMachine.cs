
using System;
using UnityEngine;

public class PA_StateMachine : EnemyMachine
{
    public EnemyState currentState;
    
    public PA_DefaultState defaultState;
    
    public PA_AttackState attackState;
    
    public PA_PursuitState pursuitState;
    
    public PA_EndPursuitState endPursuitState;
    
    public PA_PauseAttackState pauseAttackState;
    
    public PA_PausePursuitState pausePursuitState;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, defaultState.detectionDistance);
    }

    public override void OnHit()
    {
        base.OnHit();
    }
    
    public void Start()
    {
        currentState = defaultState;
        currentState.StartState(this);
    }
    
    public void Update()
    {
        currentState.UpdateState(this);
    }
    
    public void SwitchState(EnemyState state)
    {
        currentState = state;
        state.StartState(this);
    }
}
