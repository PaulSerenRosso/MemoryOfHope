using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIModule : MonoBehaviour
{
    public int index;
    
    public Sprite moduleIcon;

    public string nonAcquiredText;
    
    public string frenchModuleName;
    public string englishModuleName;

    [TextArea(5, 10)] public string frenchModuleInfo;
    [TextArea(5, 10)] public string englishModuleInfo;

    private Image moduleImage;


    private void SetData()
    {
        moduleImage.sprite = moduleIcon;

        switch (index)
        {
            // Set si on a le module ou pas
        }
    }
}
