using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestAudio : MonoBehaviour
{
    public UnityEvent baba;


    void Awake()
    {
        baba?.Invoke();
    }
}


