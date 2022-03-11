using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSource : LaserMachine
{
    [Header("LaserSource Variables")]
    [SerializeField]
    private Transform beginSource;

    public bool SourceisOpen
    {
        get
        {
            return IsActive;
        }

        set { IsActive = value;
            _triggerByLaser = value;
        }
    }
    [SerializeField]
    private Vector3 DirectionSource;
    private void Start()
    {
       
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
       else
       {
           LaserLine.SetPosition(IndexEndLineRenderer, BeginLaser);
               LaserLine.enabled = false;
           
       }
       base.FixedUpdate();
   }
}
