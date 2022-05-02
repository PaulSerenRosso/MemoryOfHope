using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIInstance : MonoBehaviour
{
    #region  Instance

    public static UIInstance instance;
    
        private void Awake()
        {
            if (instance != null && instance != this) Destroy(gameObject);
            instance = this;
        }

    #endregion

    #region Variables

    [Header("Canvas")] 
    public UICanvasType[] canvases;
    [SerializeField] private GameObject blackFilter;

    [Header("Information Menu")]
    public InGameCanvasType[] informationMenuHiddenCanvas;
    public GameObject informationWindow;
    public UIModule[] modulesGUI;
    private UIModule actualModuleGUI;
    public GameObject moduleGUIInformationBox;
    public TextMeshProUGUI moduleAbilityText;
    public TextMeshProUGUI moduleInputText;
    public TextMeshProUGUI moduleLoreText;
    public GameObject informationMenuFirstSelected;
    private Vector3 selectedModulePos;
    [SerializeField] private AnimationCurve animationSpeed;
    [SerializeField] private float speedFactor;

    [Header("Player Stats")]
    [SerializeField] private RectTransform firstHeartContainerTransform;
    [SerializeField] private float distanceBetweenHeartContainers;
    [SerializeField] private GameObject heartContainerPrefab;
    [SerializeField] private List<UIHeart> heartContainers;
    private Vector3 pos;

    [Header("Player Module")] [SerializeField]
    private RectTransform firstIconTransform;
    [SerializeField] private float distanceBetweenIcons;
    [SerializeField] private GameObject iconPrefab;
    private int displayedModule;

    [Header("Notification")] 
    [SerializeField] private GameObject notificationBox;
    [SerializeField] private TextMeshProUGUI notificationText;

    [Header("Pause Menu")]
    public InGameCanvasType[] pauseMenuHiddenCanvas;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseMenuFirstSelected;
    [SerializeField] private GameObject optionButton;

    [Header("Options Menu")]
    public InGameCanvasType[] optionMenuHiddenCanvas;
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject optionMenuFirstSelected;
    [SerializeField] private TMP_Dropdown languageDropdown;

    [Header("Navigation")]
    public EventSystem eventSystem;
    
    public List<UIDisplayText> allTextsOnScreen;

    #endregion
    
    private void Start()
    {
        InformationMenuInitialization();
        InitializationStats();
        InitializationOption();
        SetTextLanguageOnDisplay();
    }

    private void LinkInput()
    {
        GameManager.instance.inputs.UI.OpenInformationMenu.performed += InformationMenuInputPressed;
        GameManager.instance.inputs.UI.OpenPauseMenu.performed += PauseMenuInputPressed;
        GameManager.instance.inputs.UI.Return.performed += ClosingInformationMenu;
        GameManager.instance.inputs.UI.Return.performed += ClosingOptionMenu;
        GameManager.instance.inputs.UI.Return.performed += ClosingPauseMenu;
    }

    private void UnlinkInput()
    {
        GameManager.instance.inputs.UI.OpenInformationMenu.performed -= InformationMenuInputPressed;
        GameManager.instance.inputs.UI.OpenPauseMenu.performed -= PauseMenuInputPressed;
        GameManager.instance.inputs.UI.Return.performed -= ClosingInformationMenu;
        GameManager.instance.inputs.UI.Return.performed -= ClosingOptionMenu;
        GameManager.instance.inputs.UI.Return.performed -= ClosingPauseMenu;
    }
    
    private void OnEnable()
    {
        LinkInput();
        GameManager.instance.inputs.UI.Enable();
    }

    private void OnDisable()
    {
        GameManager.instance.inputs.UI.Disable();
        UnlinkInput();
    }

    #region Canvas Management

    public void SetCanvasOnDisplay(InGameCanvasType[] canvasesToSet, bool activate)
    {
        foreach (var canvasType in canvases)
        {
            foreach (var canvas in canvasesToSet)
            {
                if (canvas == canvasType.canvasType)
                {
                    canvasType.canvas.gameObject.SetActive(activate);
                }
            }
        }
    } // Active ou désactive les canvas que l'on renseigne en paramètres

    #endregion
    
    #region HUD

    public void InitializationStats()
    {
        SetInitHealth();
        DisplayHealth();
    }

    public void SetInitHealth()
    {
        heartContainers.Clear();

        if (PlayerManager.instance.maxHealth % 4 != 0)
        {
            Debug.LogError("Player Health must be a multiple of 4");
            return;
        }

        float heartsContainersNumberFloat = PlayerManager.instance.health / 4f;
        int heartsContainersNumber = (int) heartsContainersNumberFloat;
        pos = firstHeartContainerTransform.position;
        for (int i = 0; i < heartsContainersNumber; i++)
        {
            heartContainers.Add(Instantiate(heartContainerPrefab, pos, Quaternion.identity,
                firstHeartContainerTransform).GetComponent<UIHeart>());
            pos += Vector3.right * distanceBetweenHeartContainers;
        }
    }

    public void GetHeart()
    {
        heartContainers.Add(Instantiate(heartContainerPrefab, pos, Quaternion.identity,
            firstHeartContainerTransform).GetComponent<UIHeart>());
        pos += Vector3.right * distanceBetweenHeartContainers;
    }

    public void DisplayHealth()
    {
        int life = 0;
        foreach (var container in heartContainers)
        {
            foreach (var part in container.heartParts)
            {
                part.color = Color.black;
            }
        }

        int heartContainer = 0;
        int heartPart = 0;
        while (life != PlayerManager.instance.health)
        {
            life++;
            heartContainers[heartContainer].heartParts[heartPart].color = Color.red;
            heartPart++;

            if (heartPart <= 3) continue;
            heartPart = 0;
            heartContainer++;
        }
    }

    public void AddModuleIcon(Module module)
    {
        if (!module.isDisplayed) return;

        Vector3 pos = firstIconTransform.position + Vector3.right * distanceBetweenIcons * displayedModule;

        Image icon = Instantiate(iconPrefab, pos, Quaternion.identity, firstIconTransform).GetComponent<Image>();

        icon.sprite = module.moduleIconGUI;

        displayedModule++;
    }

    #endregion
    
    #region Information Menu

    private void InformationMenuInitialization()
    {
        selectedModulePos = modulesGUI[0].GetComponent<RectTransform>().anchoredPosition;
    }

    private void InformationMenuInputPressed(InputAction.CallbackContext ctx)
    {
        if (informationWindow.activeSelf) ClosingInformationMenu(ctx);
        else OpeningInformationMenu();
    }

    private void OpeningInformationMenu()
    {
        if (informationWindow.activeSelf || pauseMenu.activeSelf || optionMenu.activeSelf) return;
        SetCanvasOnDisplay(informationMenuHiddenCanvas, false);
        informationWindow.SetActive(true);
        eventSystem.SetSelectedGameObject(informationMenuFirstSelected);
        SettingPlayerState();
    }

    private void ClosingInformationMenu(InputAction.CallbackContext ctx)
    {
        if (!informationWindow.activeSelf) return;
        if (actualModuleGUI != null) StartCoroutine(UnselectModuleGUI(actualModuleGUI));
        SetCanvasOnDisplay(informationMenuHiddenCanvas, true);
        informationWindow.SetActive(false);
        eventSystem.SetSelectedGameObject(null);
        SettingPlayerState();
    }

    public void AddModuleGUI(Module module)
    {
        if (!module.isDisplayed) return;

        UIModule UImodule;
        foreach (var moduleGUI in modulesGUI)
        {
            if (moduleGUI.isUnlocked) continue;
            UImodule = moduleGUI;
            UImodule.SetData(module);
            break;
        }
    }

    public void OnModuleGUIClick(UIModule UImodule)
    {
        if (!UImodule.isUnlocked)
        {
            Debug.Log("You haven't unlocked this module yet");
            // Feedbacks
            
            return;
        }

        StartCoroutine(UImodule.isOpened ? UnselectModuleGUI(UImodule) : SelectModuleGUI(UImodule));
    }

    private IEnumerator SelectModuleGUI(UIModule UImodule)
    {
        // Retirer  au joueur
        eventSystem.SetSelectedGameObject(null);

        // Faire disparaitre les autres zones de Module GUI
        foreach (var modGUI in modulesGUI)
        {
            if (modGUI == UImodule) continue;
            modGUI.gameObject.SetActive(false);
        }
        
        // Faire monter la zone sélectionnée en haut
        var rt = UImodule.GetComponent<RectTransform>();
        var bottomY = UImodule.initialPos.y + Mathf.Abs(UImodule.initialPos.y);
        var topY = selectedModulePos.y + Mathf.Abs(UImodule.initialPos.y);
        while (bottomY < topY)
        {
            float i = bottomY / topY;
            Vector2 bonus = Vector2.up * animationSpeed.Evaluate(i) * speedFactor;
            rt.anchoredPosition += bonus;
            bottomY += bonus.y;
            yield return new WaitForSeconds(0.0025f);
        }
        rt.anchoredPosition = selectedModulePos;

        // Set les valeurs du selectedModuleGUI
        switch (SettingsManager.instance.gameLanguage)
        {
            case Language.French:
                moduleAbilityText.text = UImodule.associatedModule.frenchAbilityText;
                moduleInputText.text = UImodule.associatedModule.frenchInputText;
                moduleLoreText.text = UImodule.associatedModule.frenchLoreText;
                break;

            case Language.English:
                moduleAbilityText.text = UImodule.associatedModule.englishAbilityText;
                moduleInputText.text = UImodule.associatedModule.englishInputText;
                moduleLoreText.text = UImodule.associatedModule.englishLoreText;
                break;
        }

        // Animation d'apparition de la zone d'informations
        moduleGUIInformationBox.SetActive(true);
        yield return new WaitForSeconds(.25f);
        eventSystem.SetSelectedGameObject(UImodule.gameObject);
        actualModuleGUI = UImodule;
        UImodule.isOpened = true;
    }

    private IEnumerator UnselectModuleGUI(UIModule UImodule)
    {
        eventSystem.SetSelectedGameObject(null);

        // Faire disparaitre la zone d'informations
        moduleGUIInformationBox.SetActive(false);

        //yield return new WaitForSeconds(.25f);

        // Faire redescendre la zone à sa place
        var rt = UImodule.GetComponent<RectTransform>();
        var bottomY = UImodule.initialPos.y + Mathf.Abs(UImodule.initialPos.y);
        var topY = selectedModulePos.y + Mathf.Abs(UImodule.initialPos.y);
        while (bottomY < topY)
        {
            float i = bottomY / topY;
            Vector2 bonus = Vector2.up * animationSpeed.Evaluate(i) * speedFactor;
            rt.anchoredPosition -= bonus;
            bottomY += bonus.y;
            yield return new WaitForSeconds(0.0025f);
        }
        rt.anchoredPosition = UImodule.initialPos;

        // Faire réapparaître les autres zones de module GUI
        yield return new WaitForSeconds(.25f);
        foreach (var modGUI in modulesGUI)
        {
            if (modGUI == UImodule) continue;
            modGUI.gameObject.SetActive(true);
        }
        UImodule.isOpened = false;
        actualModuleGUI = null;

        // Redonner contrôle au joueur
        eventSystem.SetSelectedGameObject(UImodule.gameObject);
    }

    #endregion

    #region Notification

    public void SetNotification(string message, bool active)
    {
        notificationText.text = message;
        Debug.Log("je suis lu");
        notificationBox.SetActive(active);
    }

    public IEnumerator SetNotificationTime(string message, float time)
    {
        SetNotification("Life Max improved", true);
        yield return new WaitForSeconds(time);
        SetNotification(null, false);
    }

    #endregion

    #region Pause Menu

    private void PauseMenuInputPressed(InputAction.CallbackContext ctx)
    {
        if (pauseMenu.activeSelf || optionMenu.activeSelf)
        {
            ClosingOptionMenu(ctx);
            ClosingPauseMenu(ctx);
        }
        else OpeningPauseMenu();
    }
    
    public void OpeningPauseMenu()
    {
        if (pauseMenu.activeSelf) return;
        pauseMenu.SetActive(true);
        eventSystem.SetSelectedGameObject(pauseMenuFirstSelected);
        SettingPlayerState();
    }
    
    public void OnClosePauseMenu()
    {
        ClosingPauseMenu(new InputAction.CallbackContext());
    }

    public void ClosingPauseMenu(InputAction.CallbackContext ctx)
    {
        if (!pauseMenu.activeSelf) return;
        pauseMenu.SetActive(false);
        eventSystem.SetSelectedGameObject(null);
        SettingPlayerState();
    }

    public void GoingBackMainMenu()
    {
        SceneManager.instance.LoadingScene(1);
    }

    #endregion

    #region Options Menu

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
    
    public void OpeningOptionMenu()
    {
        if (optionMenu.activeSelf) return;
        pauseMenu.SetActive(false);
        optionMenu.SetActive(true);
        eventSystem.SetSelectedGameObject(optionMenuFirstSelected);
        SettingPlayerState();
    }

    public void OnCloseOptionMenu()
    {
        ClosingOptionMenu(new InputAction.CallbackContext());
    }

    public void ClosingOptionMenu(InputAction.CallbackContext ctx)
    {
        if (!optionMenu.activeSelf) return;
        optionMenu.SetActive(false);
        pauseMenu.SetActive(true);
        eventSystem.SetSelectedGameObject(optionButton);
        SettingPlayerState();
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
    }
    
    public void OnMusicChange(Slider music)
    {
        Debug.Log(music.value);
    } // Quand la valeur de l'audio (musique) est changée

    public void OnSfxChange(Slider sfx)
    {
        Debug.Log(sfx.value);
    } // Quand la valeur de l'audio (SFX) est changée

    #endregion

    public void SetTextLanguageOnDisplay()
    {
        foreach (var text in allTextsOnScreen)
        {
            text.SettingText();
        }
    }

    public void SettingPlayerState()
    {
        var active = !(informationWindow.activeSelf || pauseMenu.activeSelf || optionMenu.activeSelf);
        PlayerManager.instance.isActive = active;
        blackFilter.SetActive(!active);
        var pauseActive = pauseMenu.activeSelf || optionMenu.activeSelf;
        if (pauseActive)
        {
            GameManager.instance.inputs.UI.OpenInformationMenu.Disable();
        }
        else
        {            
            GameManager.instance.inputs.UI.OpenInformationMenu.Enable();
        }
    }
}

public enum InGameCanvasType
{
    HUDCanvas,
    InformationMenuCanvas,
    DialoguesCanvas,
    TutorialCanvas,
    PauseMenuCanvas,
    OptionsMenuCanvas, CinematicCanvas
}