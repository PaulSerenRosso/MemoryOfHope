using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhaseSO : ScriptableObject
{
    [SerializeField] private List<BossWaveSO> allWaves;
    public PhaseType phaseType;

    public void SetWave()
    {
        // Etablir l'ordre de liste de waves ?

        // Lancer la première wave
        
        // Quand la première wave est clear, lancer la prochaine
    }

    public virtual void SetPhase()
    {
        
    }
}

public enum PhaseType
{
    Invulnerable, Protected
}
