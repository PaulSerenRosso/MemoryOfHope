using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMirror : BaseMirror
{
    [Header("ShieldMirror Variables")]
    [SerializeField] private float _offsetDistanceBeginPosition;

    [SerializeField] private float Yoffset;
    
    public override void Returnable(LaserMachine laser, RaycastHit hit)
    {
        _triggerByLaser = true;   
        Direction = transform.forward;
        BeginLaser = new Vector3(transform.position.x, Yoffset, transform.position.z) +Direction*_offsetDistanceBeginPosition;
        LaserLineReceiver.SetPosition(0, hit.point);
    }
}
