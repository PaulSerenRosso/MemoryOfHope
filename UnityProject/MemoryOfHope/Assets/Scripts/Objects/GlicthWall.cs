using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlicthWall : MonoBehaviour
{
    [SerializeField] private int glitchLayer;
    [SerializeField] private int defaultLayer;

   void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerController.instance.isGlitching)
            {
                PlayerManager.instance.TriggerGlitchWall?.Invoke();
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!PlayerController.instance.isGlitching && other.gameObject.layer == glitchLayer)
            {
                other.gameObject.layer = defaultLayer;
            }
            else if (PlayerController.instance.isGlitching && other.gameObject.layer == defaultLayer)
            {
                other.gameObject.layer = glitchLayer;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.layer = defaultLayer;
        }
    }
}
