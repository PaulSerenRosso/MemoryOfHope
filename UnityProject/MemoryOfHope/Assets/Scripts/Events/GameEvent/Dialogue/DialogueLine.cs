using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class DialogueLine
{
    public CharacterDialogueProfil CharacterProfil;
    [TextArea] public string FrenchLineText;
    [TextArea] public string EnglishLineText;
    public float WaitTimeBeforeLine;
    public AudioClip VoiceLine;
    public bool IsAudioTime;
    public float CustomWaitTimeLine;
    public float VolumeScale;
}
