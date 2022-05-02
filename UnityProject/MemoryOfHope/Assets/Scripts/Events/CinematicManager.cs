using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class CinematicManager : MonoBehaviour
{
    [SerializeField]
    private Animation _fadeInOut;
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
        
        _fadeInOut.Play("BeginFade");

        StartCoroutine(WaitForLoadCinematic(index));
    }

    IEnumerator WaitForLoadCinematic(int index)
    {
        yield return new WaitForSeconds(0.25f);
          MainCameraController.Instance.MainCamera.enabled = false; 
                _director.playableAsset = AllCinematics[index];
                UIInstance.instance.SetCanvasOnDisplay( _canvasOutCinematic,false);
                UIInstance.instance.SetCanvasOnDisplay( _canvasInCinematic,true);
                InCutScene = true;
                _director.Play();
    }

    public void EndCinematic()
    {
        _fadeInOut.Play("EndFade");
        StartCoroutine(WaitForLoadGamePhase());
    }

    IEnumerator WaitForLoadGamePhase()
    {
        yield return new WaitForSeconds(1f);
           
                UIInstance.instance.SetCanvasOnDisplay( _canvasOutCinematic,true);
                UIInstance.instance.SetCanvasOnDisplay( _canvasInCinematic,false);
                MainCameraController.Instance.MainCamera.enabled = true;  
                _director.Stop();
                _director.playableAsset = null;
                InCutScene = false;
    }

  
    
}
