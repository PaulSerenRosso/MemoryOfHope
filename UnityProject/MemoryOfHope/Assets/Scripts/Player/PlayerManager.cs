using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Rigidbody rb;
    public bool canJump;
    public List<Module> obtainedModule;
    public int money;
    [SerializeField] private PlayerController playerController;
    
    public static PlayerManager instance;
    private void Awake()
    {
        if (instance is { })
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
    }

    public void AddModule(Module mod)
    {
        mod.LinkModule();
        playerController.activeModules.Add(mod);
        obtainedModule.Add(mod);
    }
        
}
