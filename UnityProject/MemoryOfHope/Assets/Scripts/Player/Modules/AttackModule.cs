using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackModule : Module
{
    [SerializeField] private List<PlayerAttackClass> attackList;
    [SerializeField] private AttackPlayerHand rightHand;
    [SerializeField] private AttackPlayerHand leftHand;
    private float attackTimer;
    private int currentIndexAttack = 0;
    bool inCombo;
    private bool canMove;
    private StateCombo currentStateCombo;

    enum StateCombo
    {
        Begin,
        WaitDamage,
        InDamage,
        WaitCombo,
        InCombo,
        End
    }

    public override void LinkModule()
    {
        Debug.Log("Linking Inputs for Attack Module");

        PlayerController.instance.playerActions.Player.Attack.started += context => InputPressed(context);
        PlayerController.instance.playerActions.Player.Attack.canceled += context => InputReleased(context);
    }

    public override bool Conditions()
    {
        if (!base.Conditions()) return false;
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
        if (inCombo) return;
        inCombo = true;
    }

    public override void Release()
    {
        
      
    }

    private void Update()
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

        currentStateCombo = StateCombo.WaitDamage;
    }

    void WaitToDamage()
    {
        List<Module> allModule = new List<Module>();
        allModule.AddRange(PlayerController.instance.activeModulesFixed);
        allModule.AddRange(  PlayerController.instance.activeModulesUpdate);
        
        for (int i = 0; i < allModule.Count; i++)
        {   if (allModule[i].inputPressed)
            {
                if (allModule[i] != this)
                {
                    PlayerController.instance.playerAnimator.Play("Idle");
                    currentStateCombo = StateCombo.End;
                    return;
                }
            }
        }
        
        if (attackTimer >= attackList[currentIndexAttack].startTimeActivateAttack)
        {
            switch (attackList[currentIndexAttack].playerAttackType)
            {
                case PlayerAttackType.LeftHand:
                {
                    leftHand.collider.enabled = true;
                    leftHand.currentDamage = attackList[currentIndexAttack].damage;
                    break;
                }
                case PlayerAttackType.RightHand:
                {
                    rightHand.currentDamage = attackList[currentIndexAttack].damage;
                    rightHand.collider.enabled = true;
                    break;
                }
                case PlayerAttackType.Both:
                {
                    leftHand.currentDamage = attackList[currentIndexAttack].damage;
                    rightHand.currentDamage = attackList[currentIndexAttack].damage;
                    rightHand.collider.enabled = true;
                    leftHand.collider.enabled = true;
                    break;
                }
            }
            

            currentStateCombo = StateCombo.InDamage;
        }
        
        
    }

    void InDamage()
    {
        if (attackTimer >= attackList[currentIndexAttack].endTimeActivateAttack)
        {
            switch (attackList[currentIndexAttack].playerAttackType)
            {
                case PlayerAttackType.LeftHand:
                {
                    leftHand.currentDamage = 0;
                    leftHand.collider.enabled = false;
                    break;
                }
                case PlayerAttackType.RightHand:
                {
                    rightHand.currentDamage = 0;
                    rightHand.collider.enabled = false;
                    break;
                }
                case PlayerAttackType.Both:
                {
                    leftHand.currentDamage = 0;
                    rightHand.currentDamage = 0;
                    rightHand.collider.enabled = false;
                    leftHand.collider.enabled = false;
                    break;
                }
            }

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
            currentStateCombo = StateCombo.WaitDamage;
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