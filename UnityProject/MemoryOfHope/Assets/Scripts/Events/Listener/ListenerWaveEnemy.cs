using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ListenerWaveEnemy : MonoBehaviour
{
    [SerializeField]
    private List<EnemyManager> _allEnemies;
    [SerializeField] private List<CheckIndexEnemiesList> checkEnemyCount;

    // Start is called before the first frame update
    void OnValidate()
    {
        for (int i = 0; i < _allEnemies.Count; i++)
        {
            _allEnemies[i].WaveListener = this;
        }
    }
    public void Raise(EnemyManager enemy)
    {
        for (int i = 0; i < checkEnemyCount.Count; i++)
        {

            if (_allEnemies.Count == checkEnemyCount[i].Index)
            {
          
                checkEnemyCount[i]?.Event.Invoke();   
                checkEnemyCount.Remove(checkEnemyCount[i]); 
                break;
            }
          
        }
        enemy.WaveListener = null;
        _allEnemies.Remove(enemy);
        
    }

    [Serializable]
    public class CheckIndexEnemiesList
    {
        public int Index;
        public UnityEvent Event;
    }
    
 
}
