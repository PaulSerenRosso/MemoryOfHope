using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    [SerializeField] Transform viewFinder;
    [SerializeField] Camera cinematicCamera;

 

    [SerializeField] private float lerpSpeed;
[SerializeField]
    private float distance;
    private Vector3 focusPoint;
    [SerializeField, Range(0f, 1f)]
    float focusCentering = 0.5f;
[SerializeField]
    private float minDistance;
    private void Awake()
    {
        focusPoint = viewFinder.position;
    }


    // Start is called before the first frame update
   

    // Update is called once per frame
    void FixedUpdate()
    {
        
        float currentDistance = Vector3.Distance(focusPoint, viewFinder.position);
            
       
            focusPoint = Vector3.Lerp(focusPoint, viewFinder.position, lerpSpeed);
        
   
        
        transform.position = focusPoint - transform.forward * distance;
    }
}
