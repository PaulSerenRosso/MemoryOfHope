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
        if (inCombo) return;
        inCombo = true;
    }

    public override void Release()
    {
    }

    public void CantCancel()
    {
        currentStateCombo = StateCombo.WaitDamage;
        /*
        if (attackTimer >= attackList[currentIndexAttack].cantCancelTime)
        {
            currentStateCombo = StateCombo.WaitDamage;
            return;
        }
        */
        
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
        /*
        List<Module> allModule = new List<Module>();
        allModule.AddRange(PlayerController.instance.activeModulesFixed);
        allModule.AddRange(PlayerController.instance.activeModulesUpdate);

       
        for (int i = 0; i < allModule.Count; i++)
        {
            if (allModule[i].inputPressed)
            {
                if (allModule[i] != this)
                {
                    PlayerController.instance.playerAnimator.Play("Idle");
                    currentStateCombo = StateCombo.End;
                    return;
                }
            }
        }
        */

        if (attackTimer >= attackList[currentIndexAttack].startTimeActivateAttack)
        {
            attackList[currentIndexAttack].attackPlayerCollider.collider.enabled = true;
            attackList[currentIndexAttack].attackPlayerCollider.currentDamage = attackList[currentIndexAttack].damage; 
            currentStateCombo = StateCombo.InDamage;
        }

       
    }

    void InDamage()
    {
        if (attackTimer >= attackList[currentIndexAttack].endTimeActivateAttack)
        {
            attackList[currentIndexAttack].attackPlayerCollider.collider.enabled = false ;
                attackList[currentIndexAttack].attackPlayerCollider.currentDamage =
                    attackList[currentIndexAttack].damage;
            
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