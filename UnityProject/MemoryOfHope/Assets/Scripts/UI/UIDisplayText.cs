using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDisplayText : MonoBehaviour
{
    private TextMeshProUGUI text;
    
    public string frenchText;
    public string englishText;

    public void SettingText()
    {
        if (SettingsManager.instance == null) return;

        text = GetComponent<TextMeshProUGUI>();

        switch (SettingsManager.instance.gameLanguage)
        {
            case Language.French:
                text.text = frenchText;
                break;
            
            case Language.English:
                text.text = englishText;
                break;
            
            default:
                Debug.LogError("Langage invalide");
                break;
        }
    }
}
