using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaserSource : LaserMachine
{
    [Header("LaserSource Variables")]
    public Transform beginSource;
    
   
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
           
           _triggerByLaser = true;
           LaserLine.enabled = true;
           BeginLaser = beginSource.position; 
           Direction = transform.TransformDirection(DirectionSource).normalized;

       }
       else
       {
           _triggerByLaser = false;
           LaserLine.SetPosition(1, BeginLaser);
               LaserLine.enabled = false;
           
       }
       base.FixedUpdate();
   }
}
