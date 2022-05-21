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
    public bool canChromatic;

    public ChromaticAberration chromaticAberration;

    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        canChromatic = true;
    }

    public IEnumerator ChromaticAberrationFeedback()
    {
        if (!canChromatic) yield return 0;
        canChromatic = false;
        
        volume.profile.TryGet(out chromaticAberration);
        
        while (chromaticAberration.intensity.value < 1)
        {
            chromaticAberration.intensity.value += 0.025f;
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(0.25f);
        
        while (chromaticAberration.intensity.value > 0)
        {
            chromaticAberration.intensity.value -= 0.025f;
            yield return new WaitForFixedUpdate();
        }

        canChromatic = true;
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
