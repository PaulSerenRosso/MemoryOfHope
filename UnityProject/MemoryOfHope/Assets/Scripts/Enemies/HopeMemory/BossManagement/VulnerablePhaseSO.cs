using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BossPhase/VulnerablePhase")]
public class VulnerablePhaseSO : BossPhaseSO
{
    public int damageToInflict;
    
    public override void SetPhase()
    {
        base.SetPhase();
    }
}