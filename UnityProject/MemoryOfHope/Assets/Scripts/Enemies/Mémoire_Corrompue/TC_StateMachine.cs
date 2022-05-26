using UnityEngine;

public class TC_StateMachine : EnemyMachine
{
    #region Variables

    float _damageLaserTimer;

    [Header("Parameters")] [SerializeField]
    float _damageLaserTime;

    [SerializeField] int _laserDamageAmount;

    public bool isHopeCorruptedTower;

    #endregion

    #region States

    public TC_DefaultState defaultState = new TC_DefaultState();
    public TC_PauseDefaultState pauseDefaultState = new TC_PauseDefaultState();

    #endregion

    #region State Machine Main Functions

    public override void Start()
    {
        currentState = pauseDefaultState;
        base.Start();
    }

    public override void OnHitByMelee()
    {
        base.OnHitByMelee();
    }

    #endregion

    #region Trigger & Collision

    public override void OnHitByLaser()
    {
        if (!enemyManager.isDead)
        {
            if (_damageLaserTimer < _damageLaserTime)
                _damageLaserTimer += Time.deltaTime;
            else
            {
                enemyManager.TakeDamage(_laserDamageAmount);
                GameManager.instance.RumbleConstant(.2f, .4f, _damageLaserTime);
                _damageLaserTimer = 0;
            }
        }
    }

    public void CancelHitLaser()
    {
        _damageLaserTimer = 0;
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
/*
        if (other.CompareTag("Shield"))
        {
            enemyManager.isBlocked = true;
            
        }
        */
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