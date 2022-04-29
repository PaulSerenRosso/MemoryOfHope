using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class CinematicManager : MonoBehaviour
{
    [SerializeField]
    private List<PlayableAsset> AllCinematics;

    [SerializeField]
    private InGameCanvasType[] _canvasOutCinematic;
    
    [SerializeField]
    private InGameCanvasType[] _canvasInCinematic;
    
    [SerializeField]
    private bool _inCutScene;

    public bool InCutScene
    {
        get => _inCutScene;
        set
        {
            _inCutScene = value;
            PlayerManager.instance.isActive = !value;
        }
    }
    [SerializeField] private PlayableDirector _director;
    public static CinematicManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        Instance = this;
    }

    public void LaunchCinematic(int index)
    {
        Debug.Log("launchCinematic");
        MainCameraController.Instance.MainCamera.enabled = false; 
        _director.playableAsset = AllCinematics[index];
        UIInstance.instance.SetCanvasOnDisplay( _canvasOutCinematic,false);
        UIInstance.instance.SetCanvasOnDisplay( _canvasInCinematic,true);
        InCutScene = true;
        _director.Play();
    }

    public void EndCinematic()
    {
       Debug.Log("test");
        UIInstance.instance.SetCanvasOnDisplay( _canvasOutCinematic,true);
        UIInstance.instance.SetCanvasOnDisplay( _canvasInCinematic,false);
        MainCameraController.Instance.MainCamera.enabled = true;  
        _director.Stop();
        _director.playableAsset = null;
        InCutScene = false;
    }

  
    
}
