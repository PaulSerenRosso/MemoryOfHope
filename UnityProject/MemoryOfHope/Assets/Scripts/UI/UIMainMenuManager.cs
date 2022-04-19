using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UIMainMenuManager : MonoBehaviour
{
    public InputMaster inputMaster;
    private MainMenuSection actualSection; 
    [SerializeField] private GameObject mainSection;
    [SerializeField] private GameObject mainSectionButton;
    
    [SerializeField] private GameObject creditsSection;
    [SerializeField] private GameObject creditsButton;

    [SerializeField] private GameObject optionsSection;
    [SerializeField] private GameObject optionsButton;

    private Dictionary<MainMenuSection, GameObject> allSections = new Dictionary<MainMenuSection, GameObject>();
    [SerializeField] private EventSystem eventSystem;
    
    private void Awake()
    {
        // A terme : mettre l'Input Master dans le GameManager
        inputMaster = new InputMaster();
    }

    public void Start()
    {
        Initialization();
        LinkInputs();
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
        inputMaster.MainMenuUI.MainMenuGoRight.performed += _ => GoingRight();
        inputMaster.MainMenuUI.MainMenuGoLeft.performed += _ => GoingLeft();
    }

    private void OnEnable()
    {
        inputMaster.Enable();
    }
    
    private void OnDisable()
    {
        inputMaster.Disable();
    }

    private void GoingRight()
    {
        Debug.Log("going right");
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

    private void GoingLeft()
    {
        Debug.Log("going left");
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
}

public enum MainMenuSection
{
    MainSection, CreditsSection, OptionsSection
}
