using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaserSource : LaserMachine
{
    [Header("LaserSource Variables")]
    public Transform beginSource;

    private bool _doActivation;
    private bool _doDesactivation;
   
    public Vector3 DirectionSource;
    private void Start()
    {
       
    }
    private void OnDrawGizmos()
    {
        if (beginSource != null)
        {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(beginSource.position, beginSource.position+transform.TransformDirection(DirectionSource).normalized);
            
        }
    }
    private void OnValidate()
    {
        if (beginSource != null)
            BeginLaser = beginSource.position;
        if(DirectionSource.magnitude != 1)
        DirectionSource = DirectionSource.normalized;
        if (IsActive) _triggerByLaser = true;
        else _triggerByLaser = false;
    }

    public override void FixedUpdate()
   {
       if (IsActive)
       {
           BeginLaser = beginSource.position; 
           Direction = transform.TransformDirection(DirectionSource).normalized;
       }
       if (IsActive && !_doActivation)
       {
           _doActivation = true;
           _doDesactivation = false;
           _triggerByLaser = true;
           LaserLine.enabled = true;

       }
       else if(!_doDesactivation && !IsActive)
       {
           _doActivation = false;
           _doDesactivation = true;
           _triggerByLaser = false;
          
               LaserLine.enabled = false;
           
       }
       base.FixedUpdate();
   }
}
