using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public List<Module> activeModules;
    [SerializeField] private List<int> currentModuleUpdate;
    [SerializeField] private List<int> currentModuleFixed;

    private void Start()
    {
        for (int i = 0; i < activeModules.Count; i++)
        {
            Module module = activeModules[i];
            module.LinkModule();
        }
    }
    void Update()
    {
    CheckActiveModule();
    CheckUpdateModule();
    }
    void FixedUpdate()
    {
        CheckFixedModule();
    }
    void CheckActiveModule()
    {
        for (int i = 0; i < activeModules.Count; i++)
        {
            if (!activeModules[i].Conditions())
                return;
            if(activeModules[i].isFixedUpdate)
                currentModuleFixed.Add(i);
            else
                currentModuleUpdate.Add(i);
        }
    }
    void CheckUpdateModule()
    {
        if (currentModuleUpdate.Count == 0)
            return;
        
        for (int i = 0; i < currentModuleUpdate.Count; i++)
            {
                activeModules[currentModuleUpdate[i]].Execute();
            }
            currentModuleUpdate.Clear();
    }
    void CheckFixedModule()
    {
        if (currentModuleFixed.Count == 0)
            return;
        
        for (int i = 0; i < currentModuleFixed.Count; i++)
        {
            activeModules[currentModuleFixed[i]].Execute();
        }
        currentModuleFixed.Clear();
    }
}
