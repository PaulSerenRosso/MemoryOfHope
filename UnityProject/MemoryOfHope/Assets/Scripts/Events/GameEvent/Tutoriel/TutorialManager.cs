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
        if (instance != null && instance != this) Destroy(gameObject);
        instance = this;
    }

    #endregion

    #region Variables
    
    public List<TutorialGameEvent> activeTutorialGameEvents;
    public TutorialGameEvent currentTutorialGameEvent;

    [SerializeField] private GameObject tutorialWindow;
    [SerializeField] private Image tutorialImage;
    [SerializeField] private TextMeshProUGUI tutorialText;

    #endregion

    #region Functions
    
    public void SetCurrentEvent()
    {
        if (activeTutorialGameEvents.Count == 0)
        {
            tutorialWindow.SetActive(false);
            return;
        }
        
        foreach (var gameEvent in activeTutorialGameEvents)
        {
            if (currentTutorialGameEvent == null)
            {
                currentTutorialGameEvent = gameEvent;
            }
            else
            {
                if (gameEvent.priority < currentTutorialGameEvent.priority) continue;
                currentTutorialGameEvent = gameEvent;
            }
        }

        if (currentTutorialGameEvent == null) return;
        SetDisplay();

    } // Set le tutoriel actuel selon les priorités des événements en cours

    public void SetDisplay()
    {
        if (currentTutorialGameEvent == null) return;

        tutorialWindow.SetActive(true);
        tutorialImage.sprite = currentTutorialGameEvent.inputSprite;

        string text;
        switch (SettingsManager.instance.gameLanguage)
        {
            case Language.French:
                text = currentTutorialGameEvent.frenchAction;
                break;
            
            case Language.English:
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
