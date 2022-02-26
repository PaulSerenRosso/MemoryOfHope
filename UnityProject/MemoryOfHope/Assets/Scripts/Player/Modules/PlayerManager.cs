using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public List<Module> obtainedModule;
    public int money;
    public List<Glitch> allGlitches;

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
        if (mod.isFixedUpdate) PlayerController.instance.activeModulesFixed.Add(mod);
        else PlayerController.instance.activeModulesUpdate.Add(mod);
        obtainedModule.Add(mod);
    }

    public void AddGlitch() // Pour tester
    {
        allGlitches.Add(new Glitch
        {
            duration = 0.5f
        });
        UIInstance.instance.SetGlitchesOnDisplay(true);


    }
}
