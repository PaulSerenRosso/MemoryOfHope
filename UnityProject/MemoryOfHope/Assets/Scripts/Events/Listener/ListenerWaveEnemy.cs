using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ListenerWaveEnemy : MonoBehaviour
{
    [SerializeField]
    private List<EnemyManager> _allEnemies;
    [SerializeField]
    private List<EnemyManager> _currentEnemies;
    [SerializeField] private List<CheckIndexEnemiesList> checkEnemyCount;

    
    // Start is called before the first frame update
  public void Awake()
    {
        _currentEnemies.Clear();
        _currentEnemies.AddRange(_allEnemies);
        for (int i = 0; i < _currentEnemies.Count; i++)
        {
            _currentEnemies[i].WaveListener = this;
        }
    }

   void Start()
    {
        EnemiesManager.Instance.WaveEnemiesList.Add(this);
    }
    public void Raise(EnemyManager enemy)
    {
        for (int i = 0; i < checkEnemyCount.Count; i++)
        {

            if (_currentEnemies.Count == checkEnemyCount[i].Index+1)
            {
          
                checkEnemyCount[i]?.Event.Invoke();   
                checkEnemyCount.Remove(checkEnemyCount[i]); 
                break;
            }
          
        }
        enemy.WaveListener = null;
        _currentEnemies.Remove(enemy);
        if (_currentEnemies.Count == 0)
        {
            EnemiesManager.Instance.WaveEnemiesList.Remove(this);
        }
        
    }

    [Serializable]
    public class CheckIndexEnemiesList
    {
        public int Index;
        public UnityEvent Event;
    }
    
 
}
