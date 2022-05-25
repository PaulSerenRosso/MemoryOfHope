using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TransitionSoundClass
{
    [SerializeField] private string _name;
    public AudioClip FinalAudio;
    public float SpeedToMinVolume;
    public float SpeedToMaxVolume;
}
