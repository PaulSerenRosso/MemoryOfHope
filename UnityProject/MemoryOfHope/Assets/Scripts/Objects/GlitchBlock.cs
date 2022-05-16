using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchBlock : MonoBehaviour
{
    private bool isGlitchAvailable;
    [SerializeField] private float duration;
    [SerializeField] private ParticleSystem feedback;

    private void Start()
    {
        feedback.Stop();
        isGlitchAvailable = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerFist") && isGlitchAvailable)
        {
            StartCoroutine(CollectingGlitch());
        }
    }

    IEnumerator CollectingGlitch()
    {
        feedback.Play();
        isGlitchAvailable = false;

        PlayerController.instance.isGlitching = true;
        PlayerController.instance.hopeCape.GetColor("Color_Hope");
        PlayerController.instance.hopeCape.SetColor("Color_Hope", PlayerController.instance.glitchColor);
        
        yield return new WaitForSeconds(duration);
        
        PlayerController.instance.isGlitching = false;
        PlayerController.instance.hopeCape.SetColor("Color_Hope", Color.black);


        yield return new WaitForSeconds(1);

        isGlitchAvailable = true;
    }
}
