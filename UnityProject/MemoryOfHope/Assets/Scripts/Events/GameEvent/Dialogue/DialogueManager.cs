using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textDialogueHolder;
    [SerializeField] private RectTransform _background;
    [SerializeField] private RectTransform _dialogueWindows;
    public static DialogueManager Instance;
    [SerializeField] private float _xOffsetBackground;
    [SerializeField] private float _yOffsetBackground;
    private const string _doublePoint = ": ";

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        Instance = this;
    }

    void Start()
    {
        _dialogueWindows.gameObject.SetActive(false);
        for (int i = 0; i < CharacterAudioSources.Count; i++)
        {
            CharacterAudioSourcesDic.Add(CharacterAudioSources[i].Character, CharacterAudioSources[i].AudioSource);
        }
    }

    [HideInInspector] public DialogueLine CurrentLine;
    [HideInInspector] public DialogueGameEvent CurrentDialogue;
    [HideInInspector] public bool InGameDialogue;
    [HideInInspector] public bool InCinematicDialogue;
    string currentLineText;
    public List<CharacterDialogueProfilAudioSource> CharacterAudioSources;
    public Dictionary<CharacterDialogueProfilEnum, AudioSource> CharacterAudioSourcesDic = new Dictionary<CharacterDialogueProfilEnum, AudioSource>();

    public void StartDialogue()
    {
        StartCoroutine(LaunchDialogue());
    }

    IEnumerator LaunchDialogue()
    {
        for (int i = 0; i < CurrentDialogue.Lines.Count; i++)
        {
            SetUpLine(i);
            yield return new WaitForSeconds(CurrentLine.WaitTimeBeforeLine);
            UpdateLine();
            UpdateBackgroundSize();
            if (CurrentLine.IsAudioTime)
                yield return new WaitForSeconds(CurrentLine.VoiceLine.length);
            else
                yield return new WaitForSeconds(CurrentLine.CustomWaitTimeLine);
        }

        EndDialogue();
    }

    void SetUpLine(int index)
    {
        _dialogueWindows.gameObject.SetActive(false);
        CurrentLine = CurrentDialogue.Lines[index];
        if (SettingsManager.instance.gameLanguage == Language.English)
            currentLineText = CurrentLine.EnglishLineText;
        else if (SettingsManager.instance.gameLanguage == Language.French)
            currentLineText = CurrentLine.FrenchLineText;
    }

    void UpdateLine()
    {
        _dialogueWindows.gameObject.SetActive(true);
        _textDialogueHolder.text = CurrentLine.CharacterProfil.Name + _doublePoint + currentLineText;
        Canvas.ForceUpdateCanvases();
        if (CurrentLine.VoiceLine != null && CharacterAudioSourcesDic.ContainsKey(CurrentLine.CharacterProfil.Character) )
        {
            Debug.Log(CurrentLine.VoiceLine);
            CharacterAudioSourcesDic[CurrentLine.CharacterProfil.Character].PlayOneShot(CurrentLine.VoiceLine);
        }
    }

    void UpdateBackgroundSize()
    {
        _background.sizeDelta = new Vector2(
            _xOffsetBackground + _textDialogueHolder.bounds.size.x,
            _textDialogueHolder.rectTransform.sizeDelta.y + _yOffsetBackground);
    }

    void EndDialogue()
    {
        InGameDialogue = false;
        InCinematicDialogue = false;
        _dialogueWindows.gameObject.SetActive(false);
    }
    //comporter l'ui
    // avoir le dialogue actuel 
    // list des audio source perszonnages class relié avec enum
}
