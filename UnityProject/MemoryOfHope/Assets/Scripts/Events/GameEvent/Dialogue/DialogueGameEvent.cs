using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueGameEvent", menuName = "Event/GameEvent/Dialogue/DialogueGameEvent", order = 1)]
public class DialogueGameEvent : ScriptableObject
{
   public List<RiposteDialogue> Ripostes;
   public void LaunchDialogue()
  {
      DialogueManager.Instance.InGameDialogue = true;
      DialogueManager.Instance.currentDialogue = this;
      DialogueManager.Instance.currentRisposte = Ripostes[0];
  }
}
