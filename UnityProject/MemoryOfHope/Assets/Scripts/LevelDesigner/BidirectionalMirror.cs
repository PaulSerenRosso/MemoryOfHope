using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BidirectionalMirror : BaseMirror
{
    [Header("BidirectionalMirror Variables")]
    [SerializeField] private BidirectionalMirror otherEntry;
    
    [SerializeField]
   private Vector3 _directionEntry;


    private void OnValidate()
    {
        if(_directionEntry.magnitude != 1)
            _directionEntry = _directionEntry.normalized;
    }

    private void OnDrawGizmos()
    {
        if (otherEntry != null)
        {
            Gizmos.color = Color.green;
        Gizmos.DrawLine(otherEntry.transform.position, otherEntry.transform.position+otherEntry.transform.TransformDirection(_directionEntry));
        }
    }

    public override void Cancel(LaserMachine laser)
    {
     
base.Cancel(laser);
        otherEntry.IsReturnLaser = false;
        otherEntry.IsActive = true;
 
    }

    public override void Returnable(LaserMachine laser, RaycastHit hit)
    {
        _triggerByLaser = true; 
        BeginLaser = otherEntry.transform.position;
        LaserLineReceiver.SetPosition(0, hit.point);
        Direction = otherEntry.transform.TransformDirection(_directionEntry).normalized;
    }

    public override void StartReturnable(LaserMachine laser, RaycastHit hit)
    {
          base.StartReturnable(laser, hit);
        otherEntry.IsReturnLaser = false;
        otherEntry.IsActive = false;
      
     
     


    }
}
