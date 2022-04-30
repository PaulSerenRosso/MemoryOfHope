using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class Feedbacks : MonoBehaviour
{
    public static Feedbacks instance;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
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

    public IEnumerator CameraShake(float duration, float magnitude)
    {
        var original = Camera.main.transform.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            Camera.main.transform.localPosition = new Vector3(x, y, original.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.localPosition = original;
    }
}
