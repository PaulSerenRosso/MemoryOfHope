using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;

public class UIMainMenuManager : MonoBehaviour
{
    private MainMenuSection actualSection; 
    [SerializeField] private GameObject mainSection;
    [SerializeField] private GameObject mainSectionButton;
    
    [SerializeField] private GameObject creditsSection;
    [SerializeField] private GameObject creditsButton;

    [SerializeField] private GameObject optionsSection;
    [SerializeField] private GameObject optionsButton;
    [SerializeField] private TMP_Dropdown languageDropdown;

    private Dictionary<MainMenuSection, GameObject> allSections = new Dictionary<MainMenuSection, GameObject>();
    [SerializeField] private EventSystem eventSystem;

    public void Start()
    {
        Initialization();
        InitializationOption();
    }

    private void Initialization()
    {
        allSections.Add(MainMenuSection.MainSection, mainSection);
        allSections.Add(MainMenuSection.CreditsSection, creditsSection);
        allSections.Add(MainMenuSection.OptionsSection, optionsSection);
        actualSection = MainMenuSection.MainSection;
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
    
    private void OnEnable()
    {
        GameManager.instance.inputs.MainMenuUI.Enable();
        LinkInputs();
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
                SetSection(MainMenuSection.OptionsSection);
                break;
            
            case MainMenuSection.CreditsSection:
                SetSection(MainMenuSection.MainSection);
                break;
            
            case MainMenuSection.OptionsSection:
                SetSection(MainMenuSection.CreditsSection);
                break;
        }
    }

    private void GoingLeft(InputAction.CallbackContext ctx)
    {
        switch (actualSection)
        {
            case MainMenuSection.MainSection:
                SetSection(MainMenuSection.CreditsSection);
                break;
            
            case MainMenuSection.CreditsSection:
                SetSection(MainMenuSection.OptionsSection);
                break;
            
            case MainMenuSection.OptionsSection:
                SetSection(MainMenuSection.MainSection);
                break;
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

    #region Main Section

    public void OnPlayClick()
    {
        if (SceneManager.instance == null) return;
        SceneManager.instance.LoadingScene(2);
    }

    public void OnQuitClick()
    {
        
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
    }

    #endregion
    
}

public enum MainMenuSection
{
    MainSection, CreditsSection, OptionsSection
}
