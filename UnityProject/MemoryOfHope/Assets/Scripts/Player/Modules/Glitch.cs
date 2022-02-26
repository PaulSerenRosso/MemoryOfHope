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
        Debug.Log("Using glitch !");

        yield return new WaitForSeconds(duration);
        
        Debug.Log("Glitch used.");
        isBeingUsed = false;
    }
}
