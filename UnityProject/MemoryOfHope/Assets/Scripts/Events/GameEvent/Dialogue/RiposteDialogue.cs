using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class RiposteDialogue
{
    public CharacterDialogueProfil CharacterProfil;
    [TextArea] public string RiposteText;
    public float WaitTimeForNext;
    public float SpeedSubtitles;
    public AudioClip AudioRiposte;
}
