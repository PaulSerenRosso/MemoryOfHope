using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Feedbacks : MonoBehaviour
{
    public static Feedbacks instance;

    private void Awake()
    {
        if (instance is { })
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
    }

    public Volume volume;
    public Vignette vignette;
    public bool canDoVignetteFeedback;

    public ChromaticAberration chromaticAberration;

    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        canDoVignetteFeedback = true;
    }

    public IEnumerator VignetteFeedbacks(float intensity, Color color)
    {
        if (canDoVignetteFeedback)
        {
            canDoVignetteFeedback = false;
            volume.profile.TryGet(out vignette);
            vignette.color.value = color;
            while (vignette.intensity.value < intensity)
            {
                vignette.intensity.value += 0.025f;
                yield return new WaitForFixedUpdate();
            }
        
            while (vignette.intensity.value > 0)
            {
                vignette.intensity.value -= 0.025f;
                yield return new WaitForFixedUpdate();
            }

            canDoVignetteFeedback = true;
        }
    }
    
    public IEnumerator ChromaticAberrationFeedback()
    {
        volume.profile.TryGet(out chromaticAberration);
        
        while (chromaticAberration.intensity.value < 1)
        {
            chromaticAberration.intensity.value += 0.025f;
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(0.5f);
        
        while (chromaticAberration.intensity.value > 0)
        {
            chromaticAberration.intensity.value -= 0.025f;
            yield return new WaitForFixedUpdate();
        }
    }
}
