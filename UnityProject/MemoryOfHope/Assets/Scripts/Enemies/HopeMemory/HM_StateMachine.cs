using UnityEngine;
using System.Collections.Generic;

public class HM_StateMachine : EnemyMachine
{

    private List<EnemyState> damageAnimationState = new List<EnemyState>();
    #region States

    [Header("Vulnerable State")]
    // IDLE
    public HM_VulnerableDefaultState vulnerableDefaultState = new HM_VulnerableDefaultState();

    // WALK
    public HM_VulnerableMoveState vulnerableMoveState = new HM_VulnerableMoveState();
    
    // CHARGE RUN
    public HM_VulnerableChargeState vulnerableChargeState = new HM_VulnerableChargeState();
    //idle
    public HM_CooldownState cooldownState = new HM_CooldownState();
    
    
    public HM_VulnerableShockwaveState vulnerableShockwaveState = new HM_VulnerableShockwaveState();

    //idle
    public HM_PauseVulnerableMove pauseVulnerableMove = new HM_PauseVulnerableMove();
    // debut de charge, shockwave
    public HM_PauseVulnerableAttack pauseVulnerableAttack = new HM_PauseVulnerableAttack();

    //hit 
    public HM_VulnerableHitState vulnerableHitState = new HM_VulnerableHitState();

    [Header("Protection State")]
    //idle
    public HM_ProtectionDefaultState protectionDefaultState = new HM_ProtectionDefaultState();

    //fin tp
    public HM_ProtectionPositionState protectionPositionState = new HM_ProtectionPositionState();
  // idle taunt
    public HM_ProtectionProtectedState protectionProtectedState = new HM_ProtectionProtectedState();

    //debut tp
    public HM_PauseProtectionPosition pauseProtectionPosition = new HM_PauseProtectionPosition();
    
    #endregion

    public List<EnemyManager> associatedTowers = new List<EnemyManager>();
    
    public int nextLifeThreshold;
    public Vector3 protectedPos;

    public bool isProtected;

    public bool isActive;

    public GameObject chargeArea;
    
    public float attackAreaLength;
    public float attackAreaHeight;

    #region State Machine Main Functions

    void Awake()
    {
        damageAnimationState.Add(vulnerableMoveState);
        damageAnimationState.Add(pauseVulnerableMove);
        damageAnimationState.Add(vulnerableDefaultState);
        damageAnimationState.Add(vulnerableHitState);
    }
    public override void Start()
    {
        isActive = false;
        protectedPos = transform.position;
    }

    public void ActivateBehaviour()
    {
        currentState = vulnerableDefaultState;
        UIInstance.instance.SetBossDisplay(enemyManager, true);
        agent.enabled = true;
        base.Start();
        isActive = true;
    }

    public void DeactivateBehaviour()
    {
        currentState = null;
        attackArea.SetActive(false);
        chargeArea.SetActive(false);
        agent.enabled = false;
        transform.position = protectedPos;
        enemyManager.Heal(enemyManager.maxHealth);
        UIInstance.instance.SetBossDisplay(enemyManager, false);
        for (int i = associatedTowers.Count - 1; i >= 0; i--)
        {
            var tower = associatedTowers[i];
            if (!tower.gameObject.activeSelf) continue;
            tower.TakeDamage(tower.maxHealth);
            tower.gameObject.SetActive(false);
            associatedTowers.Remove(tower);
        }
        
        isActive = false;
    }

    public override void OnHitByMelee()
    {
        base.OnHitByMelee();

        if (BossPhaseManager.instance.currentPhase == null) return;
        if (BossPhaseManager.instance.currentPhase.phaseType != PhaseType.Vulnerable) return;
        if (CheckDamageAnimationStateEqualCurrentState())
        {
                enemyManager.Animator.Play("Damage");
                    if (_isCurrentAttackKnockback)
                    {
                        SwitchState(vulnerableHitState);
                    }
        }
    
    }

    bool CheckDamageAnimationStateEqualCurrentState()
    {
        for (int i = 0; i < damageAnimationState.Count; i++)
        {
            if (damageAnimationState[i] == currentState)
            {
                return true;
            }
        }

        return false;
    }

    #endregion

    #region Trigger & Collision

    public override void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;
        
        currentState.OnTriggerEnterState(this, other);
        
        if (other.CompareTag("PlayerFist") && !isHit) // Hit by the player
        {
            //hitDirection = transform.position - PlayerController.instance.transform.position;
            hitDirection = -(PlayerController.instance.transform.position - transform.position);
            OnHitByMelee();
        }

        /*
        if (other.CompareTag("Shield"))
        {
            enemyManager.isBlocked = true;
        }
        */

        if (BossPhaseManager.instance.currentPhase.phaseType == PhaseType.Protected)
        {
            if (other.CompareTag("Player") /*|| other.CompareTag("Shield")*/)
            {
                hitDirection = transform.position - PlayerController.instance.transform.position;
                StartCoroutine(PlayerManager.instance.Hit(enemyManager));
            }
        }
    }

    public override void OnTriggerStay(Collider other)
    {
    }

    public override void OnTriggerExit(Collider other)
    {
        if (!isActive) return;

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