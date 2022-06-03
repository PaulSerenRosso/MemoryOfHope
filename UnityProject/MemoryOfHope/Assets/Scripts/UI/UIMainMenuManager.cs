using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIMainMenuManager : MonoBehaviour
{
    private MainMenuSection actualSection;
    [SerializeField] private GameObject mainSection;
    [SerializeField] private GameObject mainSectionButton;

    [SerializeField] private SoundUtilities _soundUtilities;
    [SerializeField] private GameObject creditsSection;
    [SerializeField] private GameObject creditsButton;

    [SerializeField] private GameObject optionsSection;
    [SerializeField] private GameObject optionsButton;
    [SerializeField] private TMP_Dropdown languageDropdown;

    [SerializeField] private UnityEvent _openOptionMenu;
    [SerializeField] private UnityEvent _openMainMenu;
    [SerializeField] private UnityEvent _openCreditMenu;

    private Dictionary<MainMenuSection, GameObject> allSections = new Dictionary<MainMenuSection, GameObject>();

    [SerializeField] private GameObject _videoBackground;
    public List<UIDisplayText> allTextsOnScreen;

    [SerializeField] private EventSystem eventSystem;

    public void Start()
    {
        StartCoroutine(WaitForAnimation());
        Initialization();
        InitializationOption();
        SetTextLanguageOnDisplay();
    }

    private void Initialization()
    {
        if (GameManager.instance.IsFirstScreen)
        {
            _videoBackground.SetActive(false);
        }
        else
        {
            _videoBackground.SetActive(true);
        }
        
        allSections.Add(MainMenuSection.MainSection, mainSection);
        allSections.Add(MainMenuSection.CreditsSection, creditsSection);
        allSections.Add(MainMenuSection.OptionsSection, optionsSection);
        actualSection = MainMenuSection.MainSection;
    }

    IEnumerator WaitForAnimation()
    {
        eventSystem.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        eventSystem.gameObject.SetActive(true);
        GameManager.instance.inputs.MainMenuUI.Enable();
        LinkInputs();
    }

    private void LinkInputs()
    {
        GameManager.instance.inputs.MainMenuUI.MainMenuGoRight.performed += GoingRight;
        GameManager.instance.inputs.MainMenuUI.MainMenuGoLeft.performed += GoingLeft;
    }

    private void UnlinkInputs()
    {
        GameManager.instance.inputs.MainMenuUI.MainMenuGoRight.performed -= GoingRight;
        GameManager.instance.inputs.MainMenuUI.MainMenuGoLeft.performed -= GoingLeft;
    }



    private void OnDisable()
    {
        GameManager.instance.inputs.MainMenuUI.Disable();
        UnlinkInputs();
    }

    private void GoingRight(InputAction.CallbackContext ctx)
    {
        switch (actualSection)
        {
            case MainMenuSection.MainSection:
            {
                SetSection(MainMenuSection.OptionsSection);
                _openOptionMenu?.Invoke();
                break;
            }


            case MainMenuSection.CreditsSection:
            {
                SetSection(MainMenuSection.MainSection);
                _openMainMenu?.Invoke();
                break;
            }


            case MainMenuSection.OptionsSection:
            {
                SetSection(MainMenuSection.CreditsSection);
                _openCreditMenu?.Invoke();
                break;
            }
        }
    }

    private void GoingLeft(InputAction.CallbackContext ctx)
    {
        switch (actualSection)
        {
            case MainMenuSection.MainSection:
            {
                SetSection(MainMenuSection.CreditsSection);
                _openCreditMenu?.Invoke();
                break;
            }


            case MainMenuSection.CreditsSection:
            {
                SetSection(MainMenuSection.OptionsSection);
                _openOptionMenu?.Invoke();
                break;
            }


            case MainMenuSection.OptionsSection:
            {
                SetSection(MainMenuSection.MainSection);
                _openMainMenu?.Invoke();
                break;
            }
        }
    }

    private void SetSection(MainMenuSection section)
    {
        foreach (var sect in allSections)
        {
            if (sect.Key == section)
            {
                sect.Value.SetActive(true);
                actualSection = sect.Key;
                continue;
            }

            sect.Value.SetActive(false);
        }

        eventSystem.SetSelectedGameObject(SetCursor());
    }

    private GameObject SetCursor()
    {
        switch (actualSection)
        {
            case MainMenuSection.MainSection:
                return mainSectionButton;

            case MainMenuSection.CreditsSection:
                return creditsButton;

            case MainMenuSection.OptionsSection:
                return optionsButton;

            default:
                Debug.LogError("Can't set any object");
                return null;
        }
    }

    public void SetTextLanguageOnDisplay()
    {
        foreach (var text in allTextsOnScreen)
        {
            text.SettingText();
        }
    }

    #region Main Section

    public void OnPlayClick()
    {
        if (SceneManager.instance == null) return;
        SceneManager.instance.LoadingScene(2);
        if (GameManager.instance.IsFirstScreen)
            SceneManager.instance.UnloadingSceneAsync(1);
    }

    public void LaunchSpecificScene(int index)
    {
        if (SceneManager.instance == null) return;
        SceneManager.instance.LoadingScene(index);
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }

    #endregion

    #region Options Section

    public void InitializationOption()
    {
        if (SettingsManager.instance == null) return;
        
        switch (SettingsManager.instance.gameLanguage)
        {
            case Language.French:
                languageDropdown.value = 0;
                break;

            case Language.English:
                languageDropdown.value = 1;
                break;

            default:
                Debug.LogError("Invalide language");
                break;
        }

        // Audio initialization values
    }

    public void OnLanguageChange(int index)
    {
        if (SettingsManager.instance == null) return;

        switch (index)
        {
            case 0:
                SettingsManager.instance.SetLanguage(Language.French);
                break;

            case 1:
                SettingsManager.instance.SetLanguage(Language.English);
                break;

            default:
                Debug.LogError("Index invalide");
                break;
        }

        SetTextLanguageOnDisplay();
    } // Quand la langue est changée

    #endregion
}

public enum MainMenuSection
{
    MainSection,
    CreditsSection,
    OptionsSection
}