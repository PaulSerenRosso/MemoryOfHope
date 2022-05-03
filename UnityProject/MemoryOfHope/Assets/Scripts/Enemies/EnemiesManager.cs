using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            BaseEnemies[i].transform.position = BaseEnemies[i].SpawnPosition;
            BaseEnemies[i].transform.rotation = BaseEnemies[i].SpawnRotation;
            BaseEnemies[i].Machine.Start();
        }
    }
    
    // change la mort des enenmis
    // mettre un state par defaut
    
    
// base enemies
//chaque enemis à une spawn 

//pool d'ennemis pour les waves; 
// list de enemis transform 

    public void CreatingEnemies(int index)
    {
        var setter = enemiesSetter[index];
        Instantiate(setter.prefab, setter.position.position, Quaternion.identity);
    }
}
