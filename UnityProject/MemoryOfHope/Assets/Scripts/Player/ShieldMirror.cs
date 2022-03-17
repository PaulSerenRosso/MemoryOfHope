using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMirror : BaseMirror
{
    [Header("ShieldMirror Variables")]
    [SerializeField] private float _offsetBeginPosition;
    
    public override void Returnable(LaserMachine laser, RaycastHit hit)
    {
        _triggerByLaser = true;   
        Direction = transform.forward;
        BeginLaser = transform.position+Direction*_offsetBeginPosition;
        LaserLineReceiver.SetPosition(0, hit.point);
    }
}
