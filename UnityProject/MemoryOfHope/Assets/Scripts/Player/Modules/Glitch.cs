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

        yield return new WaitForSeconds(duration);

        PlayerController.instance.isGlitching = false;
        isBeingUsed = false;
    }
}
