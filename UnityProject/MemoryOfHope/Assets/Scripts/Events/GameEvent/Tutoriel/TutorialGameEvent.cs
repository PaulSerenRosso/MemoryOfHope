using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Event/GameEvent/Tutorial/TutorialGameEvent", order = 1)]
public class TutorialGameEvent : ScriptableObject
{
    public Sprite inputSprite;
    public TutorialPriority priority;
    
    public string frenchAction;
    public string englishAction;

    public void SetTutorial()
    {
        TutorialManager.instance.activeTutorialGameEvents.Add(this);
        TutorialManager.instance.SetCurrentEvent();
    }

    public void RemoveTutorial()
    {
        TutorialManager.instance.activeTutorialGameEvents.Remove(this);
        TutorialManager.instance.currentTutorialGameEvent = null;
        TutorialManager.instance.SetCurrentEvent();
    }
}

public enum TutorialPriority
{
    Light, Medium, High
}
