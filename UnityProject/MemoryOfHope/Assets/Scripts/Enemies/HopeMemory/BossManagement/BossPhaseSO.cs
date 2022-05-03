using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhaseSO : ScriptableObject
{
    [SerializeField] private List<BossWaveSO> allWaves;
    [SerializeField] private BossWaveSO currentWave;
    public PhaseType phaseType;

    public virtual void SetPhase()
    {
        currentWave = allWaves[0];
        currentWave.SpawningEnemies();
    }

    public void SetNextWave()
    {
        allWaves.Remove(currentWave);
        if (allWaves.Count == 0) return;
        currentWave = allWaves[0];
        currentWave.SpawningEnemies();
    }

}

public enum PhaseType
{
    Invulnerable, Protected
}
