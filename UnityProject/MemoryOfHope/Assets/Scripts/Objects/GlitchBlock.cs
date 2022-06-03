using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlitchBlock : MonoBehaviour
{
    private bool isGlitchAvailable;
    [SerializeField] private float duration;
    [SerializeField] private ParticleSystem feedback;

    [SerializeField] private UnityEvent _closeGlitchEvent;
    [SerializeField] private UnityEvent _openGlitchEvent;

    [SerializeField] private Animation anim;
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
        anim.Play("Scene");
        feedback.Play();
        isGlitchAvailable = false;
        GameManager.instance.RumbleConstant(.2f, .6f, .5f);

        PlayerManager.instance.HasGlitchEvent?.Invoke();
        _closeGlitchEvent?.Invoke();
        PlayerController.instance.isGlitching = true;
        PlayerController.instance.hopeCape.GetColor("Color_Hope");
        PlayerController.instance.hopeCape.SetColor("Color_Hope", PlayerController.instance.glitchColor);

        yield return new WaitForSeconds(duration);

        PlayerController.instance.isGlitching = false;
        PlayerController.instance.hopeCape.SetColor("Color_Hope", Color.black);

        anim.Play("GlitchFlowerClose");

        yield return new WaitForSeconds(1);

        _openGlitchEvent?.Invoke();
        isGlitchAvailable = true;
    }
}