using UnityEngine;

public class S_StateMachine : EnemyMachine
{
    #region Variables

    public Vector3 initialPosition;
    
    [Header("Parameters")]
    [Range(1, 15)] public float detectionDistance;
    [Range(1, 15)] public float pursuitDistance;

    #endregion

    #region States

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
    
    #endregion

    #region Gizmos

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(initialPosition, detectionDistance);
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(initialPosition, pursuitDistance);
    }

    #endregion

    #region State Machine Main Functions

    public override void Start()
    {
        initialPosition = transform.position;
        currentState = defaultState;
        base.Start();
    }
    
    public override void OnHitByMelee()
    {
        base.OnHitByMelee();
        SwitchState(hitState);
    }

    #endregion

    #region Trigger & Collision

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    public override void OnTriggerStay(Collider other)
    {
        
    }

    public override void OnTriggerExit(Collider other)
    {
        
    }

    public override void OnCollisionEnter(Collision other)
    {
        
    }

    public override void OnCollisionStay(Collision other)
    {
        
    }

    public override void OnCollisionExit(Collision other)
    {
        
    }

    #endregion
}
