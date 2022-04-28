using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BossPhase/ProtectedPhase")]
public class ProtectedPhaseSO : BossPhaseSO
{
    [SerializeField] private bool isTowersRotating;
    [SerializeField] private GameObject[] corruptedTowers;

    public override void SetPhase()
    {
        
        // Faire apparaître les tours
    }
}
