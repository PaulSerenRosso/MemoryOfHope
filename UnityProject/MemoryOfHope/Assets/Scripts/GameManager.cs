using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance is { })
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        inputs = new InputMaster();
        inputs.Enable();


    }
    
    public InputMaster inputs;
    
    
    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

}
