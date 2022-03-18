using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
                Destroy(gameObject);
            Instance = this;
        
    }
    void Start()
    {
        for (int i = 0; i < CharacterAudioSources.Count; i++)
        {
            CharacterAudioSourcesDic.Add(CharacterAudioSources[i].Character, CharacterAudioSources[i].AudioSource);
        }
    }
    public RiposteDialogue currentRisposte;
    public DialogueGameEvent currentDialogue;
    public bool InGameDialogue;
    public bool InCinematicDialogue;

    public List<CharacterDialogueProfilAudioSource> CharacterAudioSources;
    public Dictionary<CharacterDialogueProfilEnum, AudioSource> CharacterAudioSourcesDic;

    //comporter l'ui
    // avoir le dialogue actuel 
    // list des audio source perszonnages class relié avec enum
}
