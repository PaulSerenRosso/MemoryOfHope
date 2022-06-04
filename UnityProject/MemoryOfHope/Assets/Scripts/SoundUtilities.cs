using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "SoundUtilites", menuName = "Sound/SoundUtilites", order = 1)]
public class SoundUtilities : ScriptableObject
{
    public AudioMixer AudioMixer;
    [SerializeField] private List<TransitionSoundClass> _transitionMusics;
    [SerializeField] private List<TransitionSoundClass> _transitionAmbiances;
    public  List<AudioClipWithName> _UISoundList = new List<AudioClipWithName>();
    
    public void SwitchMusic(int index)
    {
        SoundManager.instance.StartSwitchMusic(_transitionMusics[index]);
    }

    public void SwitchAmbiance(int index)
    {
        SoundManager.instance.StartSwitchAmbiance(_transitionAmbiances[index]);
    }

    public void PlayUISound(string name)
    {
        SoundManager.instance.SetUISound(name);
    }
    public  void SetAudioMixer(string groupName)
    {
        AudioMixer.SetFloat(groupName, Mathf.Log10(SoundManager.instance.SoundSliders[groupName].value)*20);
        PlayerPrefs.SetFloat(groupName,SoundManager.instance.SoundSliders[groupName].value );
        Debug.Log("bonsoir à tous les coucou");
    }

    public void SetAudioSliders()
    {
        
        foreach (var soundSlider in SoundManager.instance.SoundSliders)
        {
            soundSlider.Value.value = PlayerPrefs.GetFloat(soundSlider.Key); 

        }

     
    }

    [Serializable]
    public class AudioClipWithName
    {
        public AudioClip AudioClip;
        public string Name;
    }
}
