using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueGameEvent", menuName = "Event/GameEvent/Dialogue/DialogueGameEvent", order = 1)]
public class DialogueGameEvent : ScriptableObject
{
   public List<DialogueLine> Lines;
   public void SetUpDialogue()
  {
      
      DialogueManager.Instance.EndDialogue();
      DialogueManager.Instance.StopAllCoroutines();
      DialogueManager.Instance.InGameDialogue = true;
      DialogueManager.Instance.CurrentDialogue = this;
      DialogueManager.Instance.CurrentLine = Lines[0];
      DialogueManager.Instance.StartDialogue();
  }
}
