using UnityEngine;

public class PA_StateMachine : EnemyMachine
{
    #region States

    public PA_DefaultState defaultState = new PA_DefaultState();
    public PA_AttackState attackState = new PA_AttackState();
    public PA_PursuitState pursuitState = new PA_PursuitState();
    public PA_EndPursuitState endPursuitState = new PA_EndPursuitState();
    public PA_PauseAttackState pauseAttackState = new PA_PauseAttackState();
    public PA_PausePursuitState pausePursuitState = new PA_PausePursuitState();
    public PA_HitState hitState = new PA_HitState();

    #endregion

    #region Gizmos

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, defaultState.detectionDistance);
    }

    #endregion

    #region State Machine Main Functions

    public override void Start()
    {
        currentState = defaultState;
        base.Start();
    }

    public override void OnHitByMelee()
    {
        base.OnHitByMelee();
        enemyManager.Animator.Play("Damage");
        if (_isCurrentAttackKnockback) SwitchState(hitState);
    }

    #endregion

    #region Trigger & Collision

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Shield"))
        {
            enemyManager.isBlocked = true;
        }
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