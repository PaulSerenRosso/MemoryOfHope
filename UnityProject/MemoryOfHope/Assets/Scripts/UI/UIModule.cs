using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIModule : MonoBehaviour
{
    public Module associatedModule;
    public Image moduleImage;
    public Image modulePosOnMap;
    public bool isUnlocked;

    public void SetData(Module module)
    {
        isUnlocked = true;
        associatedModule = module;
        if (module.moduleIconGUI != null) moduleImage.sprite = module.moduleIconGUI;
    }
}