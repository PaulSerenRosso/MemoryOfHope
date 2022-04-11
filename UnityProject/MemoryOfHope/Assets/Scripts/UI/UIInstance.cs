using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UIInstance : MonoBehaviour
{
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

    [Header("Canvas")]
    public UICanvasType[] canvases;

    [Header("Map")]
    public GameObject map;
    public bool canMoveOnMap;
    private bool isMovingOnMap;
    private Vector2 moveOnMapVector;
    [SerializeField] private RectTransform cursorOnMap;
    [SerializeField] private Vector2[] cursorBoundaries = new Vector2[4];
    [SerializeField] private float moveMapSpeed;

    [Header("Player Stats")] 
    [SerializeField] private TextMeshProUGUI lifeText;

    [Header("Notification")] 
    [SerializeField] private GameObject notificationBox;
    [SerializeField] private TextMeshProUGUI notificationText;

    private void Start()
    {
        LinkInput();
        InitializationStats();
        InitializationMap();
    }

    private void Update()
    {
        MovingOnMap();
    }

    private void LinkInput()
    {
      PlayerController.instance.playerActions.Player.MovingOnMap.performed += InputPressedMovingOnMap;
      PlayerController.instance.playerActions.Player.MovingOnMap.canceled += InputPressedMovingOnMap;
      PlayerController.instance.playerActions.Player.OpenCloseMap.performed += _ => ClosingMap();
    }

    #region CanvasManagement

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

    #region Map

    private void InitializationMap()
    {
        RectTransform rt = map.GetComponent<RectTransform>();
        var sizeDelta = rt.sizeDelta;
        var delta = cursorOnMap.sizeDelta;
        
        Vector2 couple = new Vector2
        ((sizeDelta.x - delta.x) / 2,
            (sizeDelta.y - delta.y) / 2);
        
  

        cursorBoundaries[0] = new Vector2(couple.x, couple.y);
        cursorBoundaries[1] = new Vector2(-couple.x, couple.y);
        cursorBoundaries[2] = new Vector2(couple.x, -couple.y);
        cursorBoundaries[3] = new Vector2(-couple.x, -couple.y);

    }

    public void OpeningMap()
    {
        if (map.activeSelf) return;
        map.SetActive(true);
        canMoveOnMap = true;
    }

    private void InputPressedMovingOnMap(InputAction.CallbackContext ctx)
    {
        isMovingOnMap = ctx.performed;
        moveOnMapVector = ctx.ReadValue<Vector2>();
    }

    private void MovingOnMap()
    {
        if (canMoveOnMap && isMovingOnMap)
        {
            cursorOnMap.position += (Vector3) moveOnMapVector * moveMapSpeed;
            Debug.Log(cursorOnMap.localPosition);
            CheckBoundaries();
        }
    }

    private void CheckBoundaries()
    {
        var localPosition = cursorOnMap.localPosition;
        float posX = localPosition.x;
        float posY = localPosition.y;

        if (cursorOnMap.localPosition.x > cursorBoundaries[0].x)
        {
            posX = cursorBoundaries[0].x;
        }
        else if (cursorOnMap.localPosition.x < cursorBoundaries[1].x)
        {
            posX = cursorBoundaries[1].x;
        }
        if (cursorOnMap.localPosition.y > cursorBoundaries[0].y)
        {
            posY = cursorBoundaries[0].y;
        }
        else if (cursorOnMap.localPosition.y < cursorBoundaries[2].y)
        {
            posY = cursorBoundaries[2].y;
        }

        cursorOnMap.localPosition = new Vector3(posX, posY);

    }

    private void ClosingMap()
    {
        if (!map.activeSelf) return;
        canMoveOnMap = false;
        map.SetActive(false);
    }

    #endregion
    
    #region Player Stats

    public void InitializationStats()
    {
        DisplayLife();
    }
    
    public void DisplayLife()
    {
        if(lifeText != null)
        lifeText.text = $"Life : {PlayerManager.instance.health}";
        if (PlayerManager.instance.health == 0)
        {
            lifeText.text += " ( Dead )";
            lifeText.color = Color.red;
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
    
    public IEnumerator SetNotificationTime(string message,float time)
    {
     SetNotification("Life Max improved", true);
        yield return new WaitForSeconds(time);
       SetNotification(null, false);
    }
    #endregion
}

public enum InGameCanvasType
{
    HUDCanvas, DataMenuCanvas, DialoguesCanvas, PauseMenuCanvas, OptionsMenuCanvas
}
