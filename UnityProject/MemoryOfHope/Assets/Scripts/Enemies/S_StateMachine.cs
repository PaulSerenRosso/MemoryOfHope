using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class S_StateMachine : EnemyMachine
{
    public Vector3 initialPosition;
    
    public S_DefautState defaultState = new S_DefautState();

    public S_PursuitState pursuitState = new S_PursuitState();

    public S_EndPursuitState endPursuitState = new S_EndPursuitState();

    public S_PositionState positionState = new S_PositionState();

    public S_ApparitionState apparitionState = new S_ApparitionState();

    public S_PauseHitState pauseHitState = new S_PauseHitState();

    public S_PausePositionState pausePositionState = new S_PausePositionState();

    public S_PausePursuitState pausePursuitState = new S_PausePursuitState();

    public S_HidingState hidingState = new S_HidingState();
    
    public void Start()
    {
        initialPosition = transform.position;
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

    public void OnCollisionStay(Collision other)
    {
        currentState.OnCollisionStayState(this, other);
    }
}
