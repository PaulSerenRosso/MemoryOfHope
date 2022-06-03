using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIFirstScreenManager : MonoBehaviour
{
    [SerializeField] private float _durationLogoRubika;

    [SerializeField] private Image _inputControl;
    [SerializeField] private TextMeshProUGUI _beginText;
    [SerializeField] private TextMeshProUGUI _endText;
    [SerializeField] private float _speedFadeOut;
    private bool _isReadyToInput;
    [SerializeField] private GameObject _rubikaLogoPanel;
    [SerializeField] private AudioListener _audioListener;

    void Start()
    {
        StartCoroutine(WaitForLogoRubika());
    }

    IEnumerator WaitForLogoRubika()
    {
        yield return new WaitForSeconds(_durationLogoRubika);

        _rubikaLogoPanel.SetActive(false);
        _isReadyToInput = true;
    }

    void Update()
    {
        if (GameManager.instance.IsFirstScreen && !_isReadyToInput)
        {
            if (_beginText.color.a > 0)
            {
                Color currentColor = _beginText.color;
                currentColor.a -= Time.deltaTime * _speedFadeOut;
                _beginText.color = currentColor;
                _endText.color = currentColor;
                _inputControl.color = currentColor;
            }
        }
    }

    public void LoadingGame()
    {
        if (_isReadyToInput)
            StartCoroutine(WaitForLoadingGame());
    }

    IEnumerator WaitForLoadingGame()
    {
        _isReadyToInput = false;
        GameManager.instance.IsFirstScreen = true;
        yield return new WaitForSeconds(0.5f);

        _audioListener.enabled = false;

        SceneManager.instance.LoadingSceneAsync(1);
    }


    // Firstscreen index 0
    // Mainmenu index 1
    // Ingame index 2
    // Pause index 3
    // Options index 4
}