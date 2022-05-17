using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ConsoleManager : MonoBehaviour
{
    [SerializeField] private List<ConsoleCommand> _commands;
    [SerializeField] private bool _isDestroyOnLoad;

    [SerializeField] private Transform _content;
    [SerializeField] private GameObject _commandDescription;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private GameObject _consoleCanvas;
    private bool _isOpen;

    private float _currentTimeScale;
    public string[] InputFieldContent;
    public static ConsoleManager Instance;

 private ConsoleCommand _currentCommand ;
    

    private void Start()
    {
        if (!_isDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }

        bool isColor = false;
        for (int i = 0; i < _commands.Count; i++)
        {
            GameObject item = Instantiate(_commandDescription, _content);
            item.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = _commands[i].CommandName + ": { " + _commands[i].CommandStructure+" }";
            if (isColor)
            {
                isColor = false;
            }
            else
            {
                item.transform.GetComponent<Image>().color = new Color();
                isColor = true;
            }
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
    }

    public void OpenCloseConsole(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            if (!_isOpen)
            {
                _inputField.text = "";
                PlayerManager.instance.IsActive = false;
                _consoleCanvas.SetActive(true);
                _currentTimeScale = Time.timeScale;
                Time.timeScale = 0;
                _isOpen = true;
            }
            else
            {
                PlayerManager.instance.IsActive = true;
                _consoleCanvas.SetActive(false);
                _isOpen = false;
                Time.timeScale = _currentTimeScale;
            }
        }
    }


    public void CheckInputField()
    {
      InputFieldContent = _inputField.text.Split(char.Parse(" "));
        for (int i = 0; i < _commands.Count; i++)
        {
            if (_commands[i].IsValidated(InputFieldContent))
            {
               _inputField.textComponent.color = Color.green;
               _currentCommand = _commands[i];
             
               return;
            }
        }
        _currentCommand = null;
        _inputField.textComponent.color = Color.white;
    }

    public void EnterCommand(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            if (_currentCommand == null)
            {
                _inputField.textComponent.color = Color.red;
            }
            else
            {
                _currentCommand.Execute();  _currentCommand = null;
                _inputField.text = String.Empty;
            }
        }
    }
        
}