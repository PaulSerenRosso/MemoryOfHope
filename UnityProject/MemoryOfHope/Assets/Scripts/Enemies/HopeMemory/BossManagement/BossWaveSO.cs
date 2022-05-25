using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BossPhase/Wave/Wave")]
public class BossWaveSO : ScriptableObject
{
    [SerializeField] private GameObject[] spawningEnemies;
    [SerializeField] private int[] indexAvailablePos;
    [SerializeField] private float durationBetweenSpawns;

    [SerializeField] private List<EnemyManager> enemiesInWave = new List<EnemyManager>();

    public void SpawningEnemies()
    {
        enemiesInWave.Clear();
        var availablePos = new List<Vector3>();
        foreach (var index in indexAvailablePos)
        {
            availablePos.Add(BossPhaseManager.instance.spawningPoints[index].position);
        }

        foreach (var enemy in spawningEnemies)
        {
            if (enemy == null) continue;
            Vector3 pos = availablePos[Random.Range(0, availablePos.Count)];
            availablePos.Remove(pos);
            var manager = Instantiate(enemy, pos, Quaternion.identity).GetComponent<EnemyManager>();
            manager.IsBaseEnemy = false;
            enemiesInWave.Add(manager);
            BossPhaseManager.instance.allEnemiesInBossRoom.Add(manager);
        }
    }

    public bool IsWaveCleared()
    {
        foreach (var enemy in enemiesInWave)
        {
            if (!enemy.isDead) return false;
        }

        return true;
    }
}