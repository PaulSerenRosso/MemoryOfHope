using UnityEngine;

public class HM_StateMachine : EnemyMachine
{
    #region States

    [Header("Vulnerable State")]
    public HM_VulnerableDefaultState vulnerableDefaultState = new HM_VulnerableDefaultState();

    public HM_VulnerableMoveState vulnerableMoveState = new HM_VulnerableMoveState();
    public HM_VulnerableChargeState vulnerableChargeState = new HM_VulnerableChargeState();
    public HM_CooldownState cooldownState = new HM_CooldownState();
    public HM_VulnerableShockwaveState vulnerableShockwaveState = new HM_VulnerableShockwaveState();

    public HM_PauseVulnerableMove pauseVulnerableMove = new HM_PauseVulnerableMove();
    public HM_PauseVulnerableAttack pauseVulnerableAttack = new HM_PauseVulnerableAttack();

    public HM_VulnerableHitState vulnerableHitState = new HM_VulnerableHitState();

    [Header("Protection State")]
    public HM_ProtectionDefaultState protectionDefaultState = new HM_ProtectionDefaultState();

    public HM_ProtectionPositionState protectionPositionState = new HM_ProtectionPositionState();
    public HM_ProtectionProtectedState protectionProtectedState = new HM_ProtectionProtectedState();

    public HM_PauseProtectionPosition pauseProtectionPosition = new HM_PauseProtectionPosition();

    public HM_ProtectionHitState protectionHitState = new HM_ProtectionHitState();

    #endregion

    public int nextLifeThreshold;
    public Vector3 protectedPos;

    public bool isProtected;

    #region State Machine Main Functions

    public override void Start()
    {
        protectedPos = transform.position;
    }

    public void ActivateBehaviour()
    {
        currentState = vulnerableDefaultState;
        UIInstance.instance.SetBossDisplay(enemyManager);
        base.Start();
        // Active default state
    }

    public override void OnHitByMelee()
    {
        base.OnHitByMelee();

        switch (BossPhaseManager.instance.currentPhase.phaseType)
        {
            case PhaseType.Vulnerable:
                if (_isCurrentAttackKnockback)
                {
                    SwitchState(vulnerableHitState);
                }

                break;

            case PhaseType.Protected:
                SwitchState(protectionHitState);
                break;
        }
    }

    #endregion

    #region Trigger & Collision

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerFist") && !isHit) // Hit by the player
        {
            //hitDirection = transform.position - PlayerController.instance.transform.position;
            hitDirection = -(PlayerController.instance.transform.position - transform.position);
            OnHitByMelee();
        }

        if (other.CompareTag("Shield"))
        {
            enemyManager.isBlocked = true;
        }

        if (BossPhaseManager.instance.currentPhase.phaseType == PhaseType.Vulnerable) return;

        if (other.CompareTag("Shield"))
        {
            enemyManager.isBlocked = true;
            StartCoroutine(PlayerManager.instance.Hit(enemyManager));
        }

        if (other.CompareTag("Player") || other.CompareTag("Shield"))
        {
            hitDirection = transform.position - PlayerController.instance.transform.position;
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