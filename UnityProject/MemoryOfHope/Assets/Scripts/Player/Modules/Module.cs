using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Module : MonoBehaviour
{
    public int index;
    public bool isLinked;

    [Header("UI")]
    public bool isDisplayed;
    public Sprite moduleIconGUI;
    
    public string frenchModuleName;
    public string englishModuleName;

    [TextArea(3, 3)] public string frenchAbilityText;
    [TextArea(3, 3)] public string englishAbilityText;

    [TextArea(3, 3)] public string frenchInputText;
    [TextArea(3, 3)] public string englishInputText;
    
    [TextArea(3, 3)] public string frenchLoreText;
    [TextArea(3, 3)] public string englishLoreText;
    
    [Header("Module information")]
    
    public bool isFixedUpdate;
    public bool isPerformed;
    public List<Module> constrainingModules;
    public List<Module> neededModules;
    public bool inputPressed;
    

    public abstract void LinkModule();

    public abstract void UnlinkModule();
    
    public abstract void InputPressed(InputAction.CallbackContext ctx);

    public abstract void InputReleased(InputAction.CallbackContext ctx);

    public abstract void Cancel();
 

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
