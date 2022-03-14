using System;
using UnityEngine;

[Serializable]
public class PlayerAttackClass
{
    public float cantCancelTime;
   public  float startTimeActivateAttack;
   public float endTimeActivateAttack;
    public float startTimeCombo;
    public float endTimeCombo;
    public float damage;
   public float maxSpeedDashAttack;
   public AnimationCurve speedDashAttackCurve;
   public AttackPlayerCollider attackPlayerCollider;
     public float attackStrength;
}


