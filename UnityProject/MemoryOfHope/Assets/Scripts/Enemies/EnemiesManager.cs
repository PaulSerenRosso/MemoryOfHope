using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemiesManager : MonoBehaviour
{
    #region instance

    public static EnemiesManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        Instance = this;
    }

    #endregion

    public List<EnemyManager> BaseEnemies;

    public List<EnemySetter> enemiesSetter;

    public void RefreshBaseEnemies()
    {
        for (int i = 0; i < BaseEnemies.Count; i++)
        {
            BaseEnemies[i].gameObject.SetActive(true);
            if (BaseEnemies[i].GetComponent<NavMeshAgent>()) BaseEnemies[i].GetComponent<NavMeshAgent>().enabled = true;
            BaseEnemies[i].Machine.enabled = true;
            BaseEnemies[i].transform.position = BaseEnemies[i].SpawnPosition;
            BaseEnemies[i].transform.rotation = BaseEnemies[i].SpawnRotation;
            BaseEnemies[i].Heal(BaseEnemies[i].maxHealth);
            BaseEnemies[i].isDead = false;
            BaseEnemies[i].Machine.Start();
        }

        foreach (var enemy in BossPhaseManager.instance.allEnemiesInBossRoom)
        {
            if (!enemy.gameObject.activeSelf) continue;
            enemy.TakeDamage(enemy.maxHealth);
            enemy.gameObject.SetActive(false);
        }

        BossPhaseManager.instance.BattleRefresh();
    }

    public void CreatingEnemies(int index)
    {
        var setter = enemiesSetter[index];
        Instantiate(setter.prefab, setter.position.position, Quaternion.identity);
    }
}