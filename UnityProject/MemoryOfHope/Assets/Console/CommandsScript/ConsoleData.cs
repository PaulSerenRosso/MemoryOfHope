using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ConsoleData : MonoBehaviour
{
   public MoveModule Move;
 public float DefaultSpeed; 
    public static ConsoleData Instance;
  public List<CheckPointWithName> CheckPointList;
  public List<ModuleWithName> ModuleList; 
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            DestroyImmediate(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        DefaultSpeed = Move.defaultSpeedMovment;
    }
    [Serializable]
    public class CheckPointWithName
    {
        public string Name;
        public CheckPoint Point;
    }
    [Serializable]
    public class ModuleWithName
    {
        public string Name;
        public Module Module;
    }
}
