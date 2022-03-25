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
        Vector3 upOffset = PlayerController.instance.transform.TransformPoint(Vector3.up * Yoffset);
        BeginLaser = new Vector3(transform.position.x, upOffset.y, transform.position.z) +Direction*_offsetDistanceBeginPosition;
        LaserLineReceiver.SetPosition(0, hit.point);
    }
}
