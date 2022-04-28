using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWaveSO : ScriptableObject
{
    [SerializeField] private GameObject[] spawningEnemies;

    public void SpawningEnemies()
    {
        var positions = new List<Vector3>();
        foreach (var pos in BossPhaseManager.instance.spawningPoints)
        {
            positions.Add(pos.position);
        }

        var selectedPositions = new List<Vector3>();
        for (int i = 0; i < 3; i++)
        {
            var randomPos = positions[Random.Range(0, positions.Count + 1)];
            selectedPositions.Add(randomPos);
            positions.Remove(randomPos);
        }

        for (int i = 0; i < spawningEnemies.Length; i++)
        {
            if (spawningEnemies[i] == null) return;
            Instantiate(spawningEnemies[i], selectedPositions[i], Quaternion.identity);
        }
    }
}
