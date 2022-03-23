using System;
using System.Collections;
using UnityEngine;

public class ModuleAcquisition : MonoBehaviour
{
    #region Variables

    [SerializeField] private Module[] moduleToLearn;
    [SerializeField] private bool canBeActivated;
    [SerializeField] private bool hasBeenActivated;

    private void Start()
    {
        LinkInput();
    }

    private void LinkInput()
    {
        PlayerController.instance.playerActions.Player.Interact.performed += _ => ActivateModule();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasBeenActivated)
        {
            PlayerManager.instance.isInModule = true;
            UIInstance.instance.SetNotification("Press B to activate this module", true);
            canBeActivated = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !hasBeenActivated)
        {
            canBeActivated = false;
            PlayerManager.instance.isInModule = false;
            UIInstance.instance.SetNotification(null, false);
        }
    }

    void ActivateModule()
    {
        if (canBeActivated && !hasBeenActivated)
        {
            StartCoroutine(ActivatingModule());
        }
    }

    IEnumerator ActivatingModule()
    {
        hasBeenActivated = true;
        
        PlayerManager.instance.isInCutscene = true;
        canBeActivated = false;
        PlayerManager.instance.isInModule = false;
        UIInstance.instance.SetNotification(null, false);
        
        // Cinématique, dialogues, feedbacks ?

        foreach (var module in moduleToLearn)
        {
            if (!PlayerManager.instance.obtainedModule.Contains(module))
            {
                PlayerManager.instance.AddModule(module);
            }
        }

        yield return new WaitForSeconds(3); // Valeur arbitraire

        PlayerManager.instance.isInCutscene = false;
    }

    #endregion
}
