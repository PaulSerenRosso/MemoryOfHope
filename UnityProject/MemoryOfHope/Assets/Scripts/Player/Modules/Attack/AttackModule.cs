using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AttackModule : Module
{
    public List<PlayerAttackClass> attackList;
    private float attackTimer;
    public int currentIndexAttack = 0;
    bool inCombo;
    private bool canMove;
    private StateCombo currentStateCombo;

    private bool isTutorial;
    [SerializeField] private TutorialGameEvent attackTutorial;

    private void Start()
    {
        for (int i = 0; i < attackList.Count; i++)
        {
            for (int j = 0; j <   attackList[i].attackPlayerCollider.particleSystem.Length; j++)
            {
                attackList[i].attackPlayerCollider.particleSystem[j].Stop();
            }
            
        }
    }

    enum StateCombo
    {
        Begin,
        CantCancel,
        WaitDamage,
        InDamage,
        WaitCombo,
        InCombo,
        End
    }

    public override void LinkModule()
    {
        GameManager.instance.inputs.Player.Attack.started += InputPressed;
        GameManager.instance.inputs.Player.Attack.canceled += InputReleased;
        isLinked = true;
        isTutorial = true;
    }

    private void OnDisable()
    {
        UnlinkModule();
    }

    public override void UnlinkModule()
    {
        if (!isLinked) return;
        GameManager.instance.inputs.Player.Attack.started -= InputPressed;
        GameManager.instance.inputs.Player.Attack.canceled -= InputReleased;
    }

    public override void Cancel()
    {
        for (int i = 0; i < attackList.Count; i++)
        {
            attackList[i].attackPlayerCollider.enabled = false;
            attackList[i].attackPlayerCollider.rendererAttack.SetActive(false);
            attackList[i].attackPlayerCollider.currentDamage = 0;
        }

        EndAttack();
    }

    public override bool Conditions()
    {
        if (!base.Conditions()) return false;

        if (PlayerManager.instance.isHit)
        {
            return false; // Ne peut pas attaquer si le joueur est knockback
        }

        if (!PlayerController.instance.onGround) return false;
        return true;
    }

    public override void InputPressed(InputAction.CallbackContext ctx)
    {
        inputPressed = true;
    }

    public override void InputReleased(InputAction.CallbackContext ctx)
    {
        inputPressed = false;
        Release();
    }

    public override void Execute()
    {
        if (isTutorial)
        {
            isTutorial = false;
            attackTutorial.RemoveTutorial();
        }

        if (inCombo) return;
        inCombo = true;
    }

    public override void Release()
    {
    }

    public void CantCancel()
    {
        attackList[currentIndexAttack].FeedBackEvent?.Invoke();
        currentStateCombo = StateCombo.WaitDamage;
    }

    void Update()
    {
        if (inCombo)
        {
            switch (currentStateCombo)
            {
                case StateCombo.Begin:
                {
                    BeginAttack();
                    break;
                }
                case StateCombo.CantCancel:
                {
                    CantCancel();
                    break;
                }
                case StateCombo.WaitDamage:
                {
                    WaitToDamage();
                    break;
                }
                case StateCombo.InDamage:
                {
                    InDamage();
                    break;
                }
                case StateCombo.WaitCombo:
                {
                    WaitToCombo();
                    break;
                }
                case StateCombo.InCombo:
                {
                    InCombo();
                    break;
                }
                case StateCombo.End:
                {
                    EndAttack();
                    break;
                }
            }

            attackTimer += Time.deltaTime;
            MoveAttack();
        }
    }

    void BeginAttack()
    {
        canMove = true;
        isPerformed = true;
        PlayerController.instance.playerAnimator.SetBool("inFight", true);

     
      
        currentStateCombo = StateCombo.CantCancel;
    }

    void WaitToDamage()
    {
        if (attackTimer >= attackList[currentIndexAttack].startTimeActivateAttack)
        {
            attackList[currentIndexAttack].attackPlayerCollider.colliderAttack.enabled = true;
            attackList[currentIndexAttack].attackPlayerCollider.rendererAttack.SetActive(true);
            attackList[currentIndexAttack].attackPlayerCollider.currentDamage = attackList[currentIndexAttack].damage;
            for (int j = 0; j <   attackList[currentIndexAttack].attackPlayerCollider.particleSystem.Length; j++)
            {
                attackList[currentIndexAttack].attackPlayerCollider.particleSystem[j].Play();
            }
            currentStateCombo = StateCombo.InDamage;
        }
    }

    void InDamage()
    {
        if (attackTimer >= attackList[currentIndexAttack].endTimeActivateAttack)
        {
            attackList[currentIndexAttack].attackPlayerCollider.colliderAttack.enabled = false;
            attackList[currentIndexAttack].attackPlayerCollider.rendererAttack.SetActive(false);
            for (int j = 0; j <   attackList[currentIndexAttack].attackPlayerCollider.particleSystem.Length; j++)
            {
                attackList[currentIndexAttack].attackPlayerCollider.particleSystem[j].Stop();
            }
            attackList[currentIndexAttack].attackPlayerCollider.currentDamage = 0;

            canMove = false;
            PlayerController.instance.playerAnimator.SetInteger("currentAttack", currentIndexAttack + 1);
            currentStateCombo = StateCombo.WaitCombo;
        }
    }


    void WaitToCombo()
    {
        if (attackTimer >= attackList[currentIndexAttack].startTimeCombo)
        {
            isPerformed = false;
            currentStateCombo = StateCombo.InCombo;
        }
    }

    void InCombo()
    {
        if (attackTimer >= attackList[currentIndexAttack].endTimeCombo
            || currentIndexAttack == attackList.Count - 1)
        {
            currentStateCombo = StateCombo.End;
            return;
        }

        if (inputPressed)
        {
            inputPressed = false;
            isPerformed = true;
            canMove = true;
            attackTimer = 0;
            currentIndexAttack++;
            currentStateCombo = StateCombo.CantCancel;
            PlayerController.instance.playerAnimator.SetInteger("comboCount", currentIndexAttack + 1);
        }
    }

    void EndAttack()
    {
        isPerformed = false;
        canMove = false;
        attackTimer = 0;
        inCombo = false;
        currentIndexAttack = 0;
        PlayerController.instance.playerAnimator.SetBool("inFight", false);
        PlayerController.instance.playerAnimator.SetInteger("currentAttack", 0);
        PlayerController.instance.playerAnimator.SetInteger("comboCount", 0);
        currentStateCombo = StateCombo.Begin;
    }

    void MoveAttack()
    {
        if (canMove)
        {
            PlayerController.instance.currentVelocityWithUndo +=
                transform.forward
                * attackList[currentIndexAttack].speedDashAttackCurve.Evaluate
                    (attackTimer / attackList[currentIndexAttack].endTimeCombo)
                * attackList[currentIndexAttack].maxSpeedDashAttack;
        }
    }
}