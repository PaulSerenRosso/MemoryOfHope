using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIModule : MonoBehaviour
{
    public int index;
    
    public string nonAcquiredText;

    public Module associatedModule;
    
    public TextMeshProUGUI moduleNameText;
    
    public Image moduleImage;

    public bool isUnlocked;
    
    public void SetData(Module module)
    {
        isUnlocked = true;

        associatedModule = module;
        
        if(module.moduleIconGUI != null) moduleImage.sprite = module.moduleIconGUI;

        switch (SettingsManager.instance.gameLanguage)
        {
            case Language.French:
                moduleNameText.text = module.frenchModuleName;
                break;
            case Language.English:
                moduleNameText.text = module.englishModuleName;
                break;
        }

        GetComponent<Button>().interactable = true;
    }
    
}
