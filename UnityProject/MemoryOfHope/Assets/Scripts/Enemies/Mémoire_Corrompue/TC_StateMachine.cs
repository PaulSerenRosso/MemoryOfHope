using UnityEngine;

public class TC_StateMachine : EnemyMachine
{
    #region States

    public TC_DefaultState defaultState = new TC_DefaultState();

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
        if (other.CompareTag("Shield"))
        {
            enemyManager.isBlocked = false;
        }
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
