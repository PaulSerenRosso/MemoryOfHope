using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Event/GameEvent/Tutorial/TutorialGameEvent", order = 1)]
public class TutorialGameEvent : ScriptableObject
{
    public Sprite inputSprite;
    
    public string frenchAction;
    public string englishAction;
}
