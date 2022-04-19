using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIInstance : MonoBehaviour
{
    #region  Instance

    public static UIInstance instance;
    
        private void Awake()
        {
            if (instance is { })
            {
                DestroyImmediate(gameObject);
                return;
            }
    
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

    [Header("Player Module")] [SerializeField]
    private RectTransform firstIconTransform;
    [SerializeField] private float distanceBetweenIcons;
    [SerializeField] private GameObject iconPrefab;
    private int displayedModule;

    [Header("Notification")] [SerializeField]
    private GameObject notificationBox;
    [SerializeField] private TextMeshProUGUI notificationText;
    
    [Header("Navigation")]
    public EventSystem eventSystem;
    
    public List<TextMeshProUGUI> allTextsOnScreen;

    #endregion
    
    private void Start()
    {
        LinkInput();
        InformationMenuInitialization();
        InitializationStats();
    }

    private void LinkInput()
    {
        PlayerController.instance.playerActions.Player.OpenCloseMap.performed += _ => InformationMenuInputPressed();
    }

    #region Canvas Management

    private void SetCanvasOnDisplay(InGameCanvasType[] canvasesToSet, bool activate)
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

    #region Information Menu

    private void InformationMenuInitialization()
    {
        selectedModulePos = modulesGUI[0].GetComponent<RectTransform>().anchoredPosition;
    }

    private void InformationMenuInputPressed()
    {
        if (informationWindow.activeSelf) ClosingInformationMenu();
        else OpeningInformationMenu();
    }

    private void OpeningInformationMenu()
    {
        //SettingTimeScale(true);
        blackFilter.SetActive(true);
        SetCanvasOnDisplay(informationMenuHiddenCanvas, false);
        informationWindow.SetActive(true);
        eventSystem.SetSelectedGameObject(informationMenuFirstSelected);
    }

    private void ClosingInformationMenu()
    {
        SetCanvasOnDisplay(informationMenuHiddenCanvas, true);
        informationWindow.SetActive(false);
        eventSystem.SetSelectedGameObject(null);
        blackFilter.SetActive(false);
        //SettingTimeScale(false);
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
        eventSystem.SetSelectedGameObject(null);

        // Retirer contrôle au joueur


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
        UImodule.isOpened = true;
    }

    private IEnumerator UnselectModuleGUI(UIModule UImodule)
    {
        Debug.Log("closing");
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
        
        // Redonner contrôle au joueur

        eventSystem.SetSelectedGameObject(UImodule.gameObject);
    }

    #endregion

    #region Player Stats

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
        var pos = firstHeartContainerTransform.position;
        for (int i = 0; i < heartsContainersNumber; i++)
        {
            heartContainers.Add(Instantiate(heartContainerPrefab, pos, Quaternion.identity,
                firstHeartContainerTransform).GetComponent<UIHeart>());
            pos += Vector3.right * distanceBetweenHeartContainers;
        }
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

    public void SettingTimeScale(bool froze)
    {
        if (froze) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
}

public enum InGameCanvasType
{
    HUDCanvas,
    InformationMenuCanvas,
    DialoguesCanvas,
    TutorialCanvas,
    PauseMenuCanvas,
    OptionsMenuCanvas
}