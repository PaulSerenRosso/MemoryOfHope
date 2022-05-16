using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Glitch
{
    [HideInInspector] public bool isBeingUsed;
    [SerializeField] public float duration;
    
    public IEnumerator Execute()
    {
        isBeingUsed = true;
        PlayerController.instance.isGlitching = true;

        PlayerController.instance.hopeCape.GetColor("Color_Hope");
        PlayerController.instance.hopeCape.SetColor("Color_Hope", PlayerController.instance.glitchColor);

        
        yield return new WaitForSeconds(duration);

        PlayerController.instance.hopeCape.SetColor("Color_Hope", Color.black);
        PlayerController.instance.isGlitching = false;
        isBeingUsed = false;
    }
}
