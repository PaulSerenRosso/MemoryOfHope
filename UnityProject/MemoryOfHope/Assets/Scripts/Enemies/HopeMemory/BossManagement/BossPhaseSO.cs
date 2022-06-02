using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class BossPhaseSO : ScriptableObject
{
    [SerializeField] private BossWaveSO[] allWavesSO;
    
    private List<BossWaveSO> allWaves = new List<BossWaveSO>();

    public BossWaveSO currentWave;
    public PhaseType phaseType;
    public float rotatingSphereSpeed;

    public UnityEvent phaseEvent;

    public virtual void SetPhase()
    {
        phaseEvent?.Invoke();
        allWaves.Clear();
        currentWave = null;
        foreach (var wave in allWavesSO)
        {
            allWaves.Add(wave);
        }
        
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
    Vulnerable, Protected
}
