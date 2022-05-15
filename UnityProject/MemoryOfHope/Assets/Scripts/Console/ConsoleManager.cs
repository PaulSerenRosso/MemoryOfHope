using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConsoleManager : MonoBehaviour
{
    //[SerializeField]
  //  private ConsoleEventCommand[] _commands;
[SerializeField]
    private bool _isDestroyOnLoad;

    private float _currentTimeScale; 
    private void Start()
    {
        if (!_isDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void OpenCloseConsole()
    {
        
    }
    
    public 

    void GetAllCommandsName()
    {
        
    }
}