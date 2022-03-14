using System;
using UnityEngine;

[Serializable]
public class PlayerAttackClass
{ 
    public float startTimeActivateAttack;
   public float endTimeActivateAttack;
   public float startTimeCombo;
   public float endTimeCombo;
   public int damage;
   public float maxSpeedDashAttack;
   public AnimationCurve speedDashAttackCurve;
   public PlayerAttackType playerAttackType;
   public float attackStrength;
}

public enum PlayerAttackType
{
    RightHand, LeftHand, Both
}
