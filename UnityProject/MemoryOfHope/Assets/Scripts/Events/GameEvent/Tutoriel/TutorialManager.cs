using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    #region Instance

    public static TutorialManager instance;

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
    

    #region Variables

    public LanguageSubTitles currentLanguage;

    public TutorialGameEvent currentTutorialGameEvent;

    [SerializeField] private GameObject tutorialWindow;
    [SerializeField] private Image tutorialImage;
    [SerializeField] private TextMeshProUGUI tutorialText;

    #endregion

    #region Functions

    public void SetDisplay()
    {
        if (currentTutorialGameEvent == null) return;

        tutorialWindow.SetActive(true);
        tutorialImage.sprite = currentTutorialGameEvent.inputSprite;

        string text;
        switch (currentLanguage)
        {
            case LanguageSubTitles.French:
                text = currentTutorialGameEvent.frenchAction;
                break;
            
            case LanguageSubTitles.English:
                text = currentTutorialGameEvent.englishAction;
                break;
            
            default:
                text = null;
                break;
        }
        
        tutorialText.text = text;
    }

    #endregion
}
