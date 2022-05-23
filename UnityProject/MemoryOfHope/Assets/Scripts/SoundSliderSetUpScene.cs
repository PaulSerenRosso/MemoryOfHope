using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSliderSetUpScene : MonoBehaviour
{
    [SerializeField]
    private List<SoundSlider> _soundSlidersList = new List<SoundSlider>();
    
    void Start()
    {
        SoundManager.instance.SoundSliders.Clear();
        for (int i = 0; i < _soundSlidersList.Count; i++)
        {
           SoundManager.instance.SoundSliders.Add(_soundSlidersList[i].Name, _soundSlidersList[i].Slider);
        }
    }
}
