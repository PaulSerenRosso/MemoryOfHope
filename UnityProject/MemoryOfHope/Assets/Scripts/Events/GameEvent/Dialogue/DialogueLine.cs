using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class DialogueLine
{
    public CharacterDialogueProfil CharacterProfil;
    [TextArea] public string RiposteText;
    public float WaitTimeBeforeLine;
    public float SpeedSubtitles;
    public AudioClip AudioRiposte;
}
