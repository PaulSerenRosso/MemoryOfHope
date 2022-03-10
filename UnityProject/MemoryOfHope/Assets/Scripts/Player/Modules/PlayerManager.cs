using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : EntityDamageable, Damageable
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

    public int health { get; set; }
    public int maxHealth { get; set; }
    public bool isDead { get; set; }

    public void TakeDamage()
    {
        
    }

    public void Heal()
    {
        
    }

    public void Death()
    {
        
    }
    
    public void AddModule(Module mod)
    {
        mod.LinkModule();
        if (mod.isFixedUpdate) PlayerController.instance.activeModulesFixed.Add(mod);
        else PlayerController.instance.activeModulesUpdate.Add(mod);
        obtainedModule.Add(mod);
    }
}
