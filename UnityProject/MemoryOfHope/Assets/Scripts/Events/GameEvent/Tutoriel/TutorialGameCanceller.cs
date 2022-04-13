using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Event/GameEvent/Tutorial/TutorialGameCanceller", order = 1)]
public class TutorialGameCanceller : ScriptableObject
{
    public void CancellingTutorial()
    {
        TutorialManager.instance.currentTutorialGameEvent = null;
    }
}
