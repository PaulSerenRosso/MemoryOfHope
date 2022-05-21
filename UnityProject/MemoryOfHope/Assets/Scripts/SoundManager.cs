using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioSource _UIAudioSource;
    private Dictionary<string, AudioClip> _UISounds;
    
    public Dictionary<string, Slider> SoundSliders= new Dictionary<string, Slider>();
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _ambianceSource;

    [SerializeField]
    private SoundUtilities Utilities;
    private float _maxVolumeMusic;
    private float _maxVolumeAmbiance;
    private float _currentMaxValue;
    private AudioSource _currentSource;
    private TransitionSoundClass _currentTransition;
    private bool _inTransition;

    void Start()
    {
        for (int i = 0; i < Utilities._UISoundList.Count; i++)
        {
            _UISounds.Add(Utilities._UISoundList[i].Name, Utilities._UISoundList[i].AudioClip);
        }
        _maxVolumeAmbiance = _ambianceSource.volume;
        _maxVolumeMusic = _musicSource.volume;
        
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

    private void Update()
    {
        if (_inTransition)
        {
            TransitionSound(_currentSource, _currentMaxValue, _currentTransition);
        }
    }

   public void SetUISound(string name)
    {
        _UIAudioSource.PlayOneShot(_UISounds[name]);
    }

    public void StartSwitchMusic(TransitionSoundClass transitionSoundClass)
    {
        _currentSource = _musicSource;
        _currentMaxValue = _maxVolumeMusic;
        _currentTransition = transitionSoundClass;
        _inTransition = true;
    }


   public void StartSwitchAmbiance(TransitionSoundClass transitionSoundClass)
    {
        _currentSource = _ambianceSource;
        _currentMaxValue = _maxVolumeAmbiance;
        _currentTransition = transitionSoundClass;
        _inTransition = true;
    }

    void TransitionSound(AudioSource source, float maxValue, TransitionSoundClass transition)
    {
        if (source.volume > 0 && source.clip != transition.FinalAudio)
            source.volume -= transition.SpeedToMinVolume * Time.deltaTime;
        else
        {
            if (source.clip != transition.FinalAudio)
            {
                source.clip = transition.FinalAudio;
                source.Play();
            }
            else
            {
                if (source.volume < maxValue)
                {
                    source.volume += transition.SpeedToMaxVolume * Time.deltaTime;
                }
                else
                {
                    _inTransition = false;
                }
            }
        }
    }




}