using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchBlock : MonoBehaviour
{
    private bool isGlitchAvailable;
    [SerializeField] private float duration;
    public Material material;
    [SerializeField] private Color availableGlitchColor;
    [SerializeField] private Color usedGlitchColor;
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
        material.color = usedGlitchColor;
        
        yield return new WaitForSeconds(duration);
        
        PlayerController.instance.isGlitching = false;

        yield return new WaitForSeconds(1);

        material.color = availableGlitchColor;
        isGlitchAvailable = true;
    }
}
