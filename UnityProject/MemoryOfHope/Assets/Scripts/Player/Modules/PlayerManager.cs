using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, Damageable
{
    #region Variables

    public List<Module> obtainedModule;
    public int money;
    public bool hasGlitch;
    
    public int health { get; set; }
    public int maxHealth { get; set; }
    public bool isDead { get; set; }

    #endregion

    #region Instance

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

    #endregion

    #region Main Functions


    public void TakeDamage(int damages)
    {
        
    }

    public void Heal(int heal)
    {
        
    }

    public void Death()
    {
        
    }

    #endregion

    #region Modules

    public void AddModule(Module mod)
    {
        mod.LinkModule();
        if (mod.isFixedUpdate) PlayerController.instance.activeModulesFixed.Add(mod);
        else PlayerController.instance.activeModulesUpdate.Add(mod);
        obtainedModule.Add(mod);
    }

    #endregion

    #region Trigger & Collision

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        
    }

    private void OnCollisionStay(Collision other)
    {
        
    }


    private void OnCollisionExit(Collision other)
    {
        
    }

    #endregion
    
}
