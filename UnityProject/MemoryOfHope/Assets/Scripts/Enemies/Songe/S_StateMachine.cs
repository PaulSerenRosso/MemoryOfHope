using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_StateMachine : EnemyMachine
{
    #region Variables

    public Vector3 initialPosition;
    [SerializeField] private GameObject[] _renderers;
    float _damageLaserTimer;

    [Header("Parameters")] [SerializeField]
    float _damageLaserTime;

    [SerializeField] int _laserDamageAmount;
    [Range(1, 15)] public float detectionDistance;
    [Range(1, 15)] public float pursuitDistance;
    
    public ParticleSystem hazardousEffect;

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
    [SerializeField] float _startHidenTime;

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
        for (var i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].SetActive(false);
        }

        initialPosition = transform.position;
        currentState = hidingState;
        base.Start();
        StartCoroutine(WaitForEndHiddenState());
    }

    IEnumerator WaitForEndHiddenState()
    {
        yield return new WaitForSeconds(_startHidenTime);
        for (var i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].SetActive(true);
        }
    }

    public override void OnHitByMelee()
    {
        base.OnHitByMelee();
        if (_isCurrentAttackKnockback)
        {
            enemyManager.Animator.SetBool("IsDamage", true);
            currentState.CancelHit(this);
            SwitchState(hitState);
        }
    }

    #endregion

    #region Trigger & Collision

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        /*
        if (other.CompareTag("Shield"))
        {
            enemyManager.isBlocked = true;
            StartCoroutine(PlayerManager.instance.Hit(enemyManager));
        }
        */

        if (other.CompareTag("Player") /*|| other.CompareTag("Shield")*/)
        {
            hitDirection = transform.position - PlayerController.instance.transform.position;
            SwitchState(pauseHitState);
        }
    }

    public override void OnHitByLaser()
    {
        if (_damageLaserTimer < _damageLaserTime)
            _damageLaserTimer += Time.deltaTime;
        else
        {
            enemyManager.TakeDamage(_laserDamageAmount);
            _damageLaserTimer = 0;
        }
    }

    public void CancelHitLaser()
    {
        _damageLaserTimer = 0;
    }

    public override void OnTriggerStay(Collider other)
    {
    }

    public override void OnTriggerExit(Collider other)
    {
        /*
        if (other.CompareTag("Shield"))
        {
            enemyManager.isBlocked = false;
        }
        */
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