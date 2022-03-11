using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class S_StateMachine : EnemyMachine
{
    public Vector3 initialPosition;
    
    [Header("Parameters")]
    [Range(1, 15)] public float detectionDistance;
    [Range(1, 15)] public float pursuitDistance;
    
    public S_DefautState defaultState = new S_DefautState();

    public S_PursuitState pursuitState = new S_PursuitState();

    public S_EndPursuitState endPursuitState = new S_EndPursuitState();

    public S_PositionState positionState = new S_PositionState();

    public S_ApparitionState apparitionState = new S_ApparitionState();

    public S_PauseHitState pauseHitState = new S_PauseHitState();

    public S_PausePositionState pausePositionState = new S_PausePositionState();

    public S_PausePursuitState pausePursuitState = new S_PausePursuitState();

    public S_HidingState hidingState = new S_HidingState();

    public S_HitState hitState = new S_HitState();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(initialPosition, detectionDistance);
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(initialPosition, pursuitDistance);

    }

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
        currentState.StartState(this);
    }

    public void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentState.OnCollisionStayState(this, other);
        }
    }
    
    public override void OnHit()
    {
        base.OnHit();
        SwitchState(hitState);
    }
}
