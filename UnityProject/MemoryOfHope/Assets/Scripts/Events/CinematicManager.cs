using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.UI;

public class CinematicManager : MonoBehaviour
{
    [SerializeField] private Animation _fadeInOut;
    [SerializeField] private List<PlayableAsset> AllCinematics;

    [SerializeField] private InGameCanvasType[] _canvasOutCinematic;

    [SerializeField] private InGameCanvasType[] _canvasInCinematic;

    [SerializeField] private bool _inCutScene;


    [SerializeField] Slider _skipSlider;
    [SerializeField] private float _skipSliderSpeed;

    private bool _isPressSkipInput;

    [SerializeField] private GameObject[] modules;

    private void Start()
    {
        GameManager.instance.inputs.UI.SkipCinematic.performed += SkipCinematic;
        GameManager.instance.inputs.UI.SkipCinematic.canceled += CancelCinematic;
    }

    void SkipCinematic(InputAction.CallbackContext context)
    {
        _isPressSkipInput = true;
    }

    void CancelCinematic(InputAction.CallbackContext context)
    {
        _isPressSkipInput = false;
        _skipSlider.value = 0;
    }

    private void Update()
    {
        if (_inCutScene)
        {
            if (_isPressSkipInput)
            {
                _skipSlider.value += Time.deltaTime * _skipSliderSpeed;
                if (_skipSlider.value >= _skipSlider.maxValue)
                {
                    EndCinematic();
                }
            }
        }
    }

    public bool InCutScene
    {
        get => _inCutScene;
        set
        {
            _inCutScene = value;
            PlayerManager.instance.IsActive = !value;
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
        PlayerManager.instance.speedEffect.gameObject.SetActive(false);
        _skipSlider.value = 0;

        UIInstance.instance.SetCanvasOnDisplay(_canvasOutCinematic, false);
        UIInstance.instance.SetCanvasOnDisplay(_canvasInCinematic, true);
        _skipSlider.transform.parent.gameObject.SetActive(true);
        StartCoroutine(WaitForLoadCinematic(index));
    }

    IEnumerator WaitForLoadCinematic(int index)
    {
        yield return new WaitForSeconds(0.25f);
        MainCameraController.Instance.MainCamera.enabled = false;
        _director.playableAsset = AllCinematics[index];

        InCutScene = true;
        _director.Play();
    }

    public void EndCinematic()
    {
        _fadeInOut.Play("EndFade");
        DialogueManager.Instance.EndDialogue();
        DialogueManager.Instance.StopAllCoroutines();
        PlayerManager.instance.speedEffect.gameObject.SetActive(true);
        _skipSlider.transform.parent.gameObject.SetActive(false);
        StartCoroutine(WaitForLoadGamePhase());
    }

    IEnumerator WaitForLoadGamePhase()
    {
        yield return new WaitForSeconds(1f);

        UIInstance.instance.SetCanvasOnDisplay(_canvasOutCinematic, true);
        UIInstance.instance.SetCanvasOnDisplay(_canvasInCinematic, false);
        MainCameraController.Instance.MainCamera.enabled = true;
        _director.Stop();
        _director.playableAsset = null;
        InCutScene = false;
    }

    public void ActivateModuleFeedback(GameObject module) // A appeler au bon moment dans les cinématiques
    {
        var moduleAcquisition = module.GetComponent<ModuleAcquisition>();
        moduleAcquisition.activateModuleEffect.Play();
    }
}