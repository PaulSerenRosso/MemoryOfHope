using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIInstance : MonoBehaviour
{
    #region Instance

    public static UIInstance instance;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        instance = this;
    }

    #endregion

    #region Variables

    [Header("Canvas")] public UICanvasType[] canvases;
    public Animation blackFilter;

    [Header("Information Menu")] public InGameCanvasType[] informationMenuHiddenCanvas;
    public GameObject informationWindow;
    public UIModule[] modulesGUI;
    private UIModule actualModuleGUI;
    public TextMeshProUGUI moduleAbilityText;
    public TextMeshProUGUI moduleInputText;
    public TextMeshProUGUI moduleLoreText;
    public TextMeshProUGUI moduleNameText;
    public GameObject informationMenuFirstSelected;
    [SerializeField] private Sprite cursor;
    [SerializeField] private Sprite emptyCursor;

    [SerializeField] private TextMeshProUGUI hp_m14Text;
    public int respawnCount = 1;

    [Header("Player Stats")] [SerializeField]
    private RectTransform firstHeartContainerTransform;

    [SerializeField] private float distanceBetweenHeartContainers;
    [SerializeField] private GameObject heartContainerPrefab;
    [SerializeField] private List<UIHeart> heartContainers;
    private Vector3 pos;
    
    [Header("Player Module")] [SerializeField]
    private RectTransform firstIconTransform;

    public Slider LaserSlider;

    [SerializeField] private float distanceBetweenIcons;
    [SerializeField] private GameObject iconPrefab;
    private int displayedModule;

    [Header("Notification")] [SerializeField]
    private GameObject notificationBox;

    [SerializeField] private TextMeshProUGUI notificationText;

    [Header("Pause Menu")] public InGameCanvasType[] pauseMenuHiddenCanvas;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseMenuFirstSelected;
    [SerializeField] private GameObject optionButton;

    [Header("Options Menu")] public InGameCanvasType[] optionMenuHiddenCanvas;
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject optionMenuFirstSelected;
    [SerializeField] private TMP_Dropdown languageDropdown;


    [Header("Boss Canvas")] public Slider bossLifeGauge;
    public Image fillImage;

    [Header("Navigation")] public EventSystem eventSystem;

    public List<UIDisplayText> allTextsOnScreen;

    #endregion

    private void Start()
    {
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
            container.GetComponent<Image>().sprite = container.heartParts[0];
        }

        int heartContainer = 0;
        int heartPart = 0;
        while (life != PlayerManager.instance.health)
        {
            for (int i = 1; i <= 4; i++)
            {
                life++;
                var heart = heartContainers[heartContainer];
                heart.GetComponent<Image>().sprite = heart.heartParts[i];

                if (life == PlayerManager.instance.health) break;
            }

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

    private void InformationMenuInputPressed(InputAction.CallbackContext ctx)
    {
        if (informationWindow.activeSelf) ClosingInformationMenu(ctx);
        else OpeningInformationMenu();
    }

    private void OpeningInformationMenu()
    {
        if (!PlayerManager.instance.IsActive) return;
        if (informationWindow.activeSelf || pauseMenu.activeSelf || optionMenu.activeSelf) return;
        SetCanvasOnDisplay(informationMenuHiddenCanvas, false);

        hp_m14Text.text = $"HP-M14 - V{respawnCount}.0";
        informationWindow.SetActive(true);
        eventSystem.SetSelectedGameObject(informationMenuFirstSelected);
        SettingPlayerState();
    }

    private void ClosingInformationMenu(InputAction.CallbackContext ctx)
    {
        if (!informationWindow.activeSelf) return;
        //if (actualModuleGUI != null) StartCoroutine(UnselectModuleGUI(actualModuleGUI));
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

    public void OnModuleGUIHover(UIModule UImodule)
    {
        foreach (var modGUI in modulesGUI)
        {
            if (!modGUI.isUnlocked) continue;
            moduleAbilityText.text = null;
            moduleInputText.text = null;
            moduleLoreText.text = null;
            moduleNameText.text = "???";
            modGUI.modulePosOnMap.sprite = emptyCursor;
        }

        if (UImodule.isUnlocked)
        {
            UImodule.modulePosOnMap.sprite = cursor;

            switch (SettingsManager.instance.gameLanguage)
            {
                case Language.French:
                    moduleAbilityText.text = UImodule.associatedModule.frenchAbilityText;
                    moduleInputText.text = UImodule.associatedModule.frenchInputText;
                    moduleLoreText.text = UImodule.associatedModule.frenchLoreText;
                    moduleNameText.text = UImodule.associatedModule.frenchModuleName;
                    break;

                case Language.English:
                    moduleAbilityText.text = UImodule.associatedModule.englishAbilityText;
                    moduleInputText.text = UImodule.associatedModule.englishInputText;
                    moduleLoreText.text = UImodule.associatedModule.englishLoreText;
                    moduleNameText.text = UImodule.associatedModule.englishModuleName;
                    break;
            }
        }
        else
        {
            switch (SettingsManager.instance.gameLanguage)
            {
                case Language.French:
                    moduleAbilityText.text = "Vous n'avez pas encore débloqué ce module...";
                    break;
                case Language.English:
                    moduleAbilityText.text = "You haven't unlocked this module yet...";
                    break;
            }
        }
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
        if (!PlayerManager.instance.IsActive) return;
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
       
    } // Quand la valeur de l'audio (musique) est changée

    public void OnSfxChange(Slider sfx)
    {
        Debug.Log(sfx.value);
    } // Quand la valeur de l'audio (SFX) est changée

    #endregion

    #region Boss

    public void SetBossDisplay(EnemyManager enemy, bool active)
    {
        bossLifeGauge.maxValue = enemy.maxHealth;
        bossLifeGauge.value = bossLifeGauge.maxValue;
        bossLifeGauge.gameObject.SetActive(active);
    }

    public void SetBossLifeGauge(PhaseType type)
    {
        switch (type)
        {
            case PhaseType.Vulnerable:
                fillImage.color = Color.red;
                break;
            case PhaseType.Protected:
                fillImage.color = Color.grey;
                break;
        }
    }

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
        PlayerManager.instance.IsActive = active;
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
    OptionsMenuCanvas,
    CinematicCanvas
}