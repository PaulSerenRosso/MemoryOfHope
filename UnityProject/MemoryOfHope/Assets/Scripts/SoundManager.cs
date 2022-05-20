using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioSource _UIAudioSource;
    [SerializeField]
   private List<AudioClipWithName> _UISoundList;
     private Dictionary<string, AudioClip> _UISounds;
      public AudioMixer AudioMixer;
      [SerializeField] private List<SoundSlider> _soundSliders;
      [SerializeField] private AudioSource _musicSource ;
      [SerializeField] private AudioSource _ambianceSource ;
      [SerializeField] private List<TransitionSound> _transitionSounds;
     void Start()
    {
        for (int i = 0; i < _UISoundList.Count; i++)
        {
            _UISounds.Add(_UISoundList[i].Name,_UISoundList[i].AudioClip );
        }
    }
    private void Awake()
    {
        if (instance is { })
        {
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;
        
        DontDestroyOnLoad(gameObject);
    }

    public void SetUISound(string name)
    {
       _UIAudioSource.PlayOneShot(_UISounds[name]); 
    }

    public void SwitchMusic(int index)
    {
        
    }
 

    public void SwitchAmbiance(int index)
    {
        
    }

    public void SetAudioMixer(string groupName, float value)
    {
        AudioMixer.SetFloat(groupName, value);
    }

    public class AudioClipWithName
    {
        public AudioClip AudioClip;
        public string Name; 
    }
}
