using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class CinematicManager : MonoBehaviour
{
    [SerializeField]
    private List<PlayableAsset> AllCinematics;

    private bool _inCutScene;

    public bool InCutScene
    {
        get => _inCutScene;
        set
        {
            _inCutScene = value;
            PlayerManager.instance.isActive = value;
        }
    }
    [SerializeField] private PlayableDirector _director;
    public static CinematicManager Instance;
    private void Awake()
    {
        if (Instance is { })
        {
            DestroyImmediate(gameObject);
            return;
        }
        Instance = this;
    }

    public void LaunchCinematic(int index)
    {
        _director.playableAsset = AllCinematics[index];
        InCutScene = true;
        _director.Play();
    }

    public void EndCinematic()
    {
        _director.playableAsset = null;
        InCutScene = false;
    }
    
}
