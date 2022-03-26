using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
   public static MainCameraController Instance;
   [SerializeField] Transform viewFinder;
    [SerializeField]
    private float distance;
    [SerializeField]
    Vector3 offSet;
    private void Awake()
    {
        if (Instance is { })
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
    }


    // Start is called before the first frame update
   

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = viewFinder.position+offSet - transform.forward * distance;
    }
 
}
