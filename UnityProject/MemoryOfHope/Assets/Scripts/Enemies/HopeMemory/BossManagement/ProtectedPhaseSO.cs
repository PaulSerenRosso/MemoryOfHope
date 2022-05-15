using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BossPhase/ProtectedPhase")]
public class ProtectedPhaseSO : BossPhaseSO
{
    [SerializeField] private bool isTowersRotating;
    [SerializeField] private GameObject corruptedTowers;
    
    public override void SetPhase()
    {
        base.SetPhase();

        foreach (var tr in BossPhaseManager.instance.towersSpawningPoints)
        { 
            Instantiate(corruptedTowers, tr.position, Quaternion.identity, tr);
        }

        BossPhaseManager.instance.isSphereRotating = isTowersRotating;
    }
}
