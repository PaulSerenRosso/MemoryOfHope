using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFade : MonoBehaviour
{
    [SerializeField] private GameObject solidBody;
    [SerializeField] private GameObject transperantBody;

    private void Awake()
    {
        ShowSolid();
    }

    public void ShowTransperant()
    {
        solidBody.SetActive(false);
        transperantBody.SetActive(true);
    }

    public void ShowSolid()
    {
        solidBody.SetActive(true);
        transperantBody.SetActive(false);
    }
}

