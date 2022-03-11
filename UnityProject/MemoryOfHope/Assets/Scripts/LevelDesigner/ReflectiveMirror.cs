using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectiveMirror : LaserMachine, IReturnable
{


    public bool IsReturnLaser
    {
        get
        {
            return _triggerByLaser;
        }
        
        set { _triggerByLaser = value; }
    }
    
 

    public virtual void Returnable(LaserMachine laser, RaycastHit hit)
    {
      _triggerByLaser = true; 
        BeginLaser = hit.point;
        Direction = Vector3.Reflect(laser.Direction, hit.normal);
    }

    public void Cancel(LaserMachine laser)
    {   EndTrigger();
        BeginLaser = Vector3.zero;
      _triggerByLaser = false; 
        Direction = Vector3.zero;
        LaserLine.positionCount = IndexEndLineRenderer;
        Debug.Log(LaserLine.positionCount);
        IndexBeginLineRenderer = 0;
        IndexEndLineRenderer =0;
        LaserLine = null;
      
    }

    public void StartReturnable(LaserMachine laser, RaycastHit hit)
    {
        _triggerByLaser = true; 
            IsActive = true;
            LaserLine = laser.LaserLine;
        IndexBeginLineRenderer = LaserLine.positionCount - 1;
      
        IndexEndLineRenderer = LaserLine.positionCount;
        LaserLine.positionCount = LaserLine.positionCount + 1;
    }
}
