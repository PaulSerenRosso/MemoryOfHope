using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Module : MonoBehaviour
{
    public int index;
    public bool isFixedUpdate;
    public bool isPerformed;
    public List<Module> constrainingModules;
    public List<Module> neededModules;
    
    public bool inputPressed;

    public abstract void LinkModule();
    
    public abstract void InputPressed(InputAction.CallbackContext ctx);

    public abstract void InputReleased(InputAction.CallbackContext ctx);

    public virtual bool Conditions()
    {
        if (!CheckConstraintModules())
        {
            return false; // Faux si un module contraint
        }
        if (!CheckNeededModules())
        { 
            return false; // Faux si un module nécessaire est manquant
        }
        if (!CheckInput())
        { 
            return false; // Faux si pas d'input pressé
        }
        return true;
    }

    public abstract void Execute();

    public abstract void Release();

     bool CheckConstraintModules()
    {
        // Check des modules en cours d'utilisation
        if (constrainingModules.Count != 0)
        {
            for (int i = 0; i < constrainingModules.Count; i++)
            {
                Module module = constrainingModules[i];
                if (module.isPerformed)
                {
                    return false;
                }
            }
        }
        return true;
    }
    
    public bool CheckNeededModules()
    {
        // Check des modules en cours d'utilisation
        if (neededModules.Count != 0)
        {
            for (int i = 0; i < neededModules.Count; i++)
            {
                Module module = neededModules[i];
                if (!module.isPerformed)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool CheckInput()
    {
        return inputPressed;
    }
    
}
