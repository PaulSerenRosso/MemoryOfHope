using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMirror: LaserMachine, IReturnable
{
      public LineRenderer LaserLineReceiver;
    public virtual bool IsReturnLaser
    {
        get
        {
            return _triggerByLaser;
        }
        set { _triggerByLaser = value; }
    }

    public bool IsActiveReturnable
    {
        get => IsActive;
        set
        {
           IsActive = value ; 
         
        } 
    }

    public LaserMachine _currentSource;
    public LaserMachine CurrentSource { get=>_currentSource; set => _currentSource = value; }

    public virtual void Returnable(LaserMachine laser, RaycastHit hit)
    {
      _triggerByLaser = true; 
      LaserLineReceiver.SetPosition(0, hit.point);
        BeginLaser = hit.point;
        Direction = Vector3.Reflect(laser.Direction, hit.normal);
    }

    public virtual void Cancel(LaserMachine laser)
    {   EndTrigger();
        BeginLaser = Vector3.zero;
        Direction = Vector3.zero;
        LaserLine.enabled = false;
      _triggerByLaser = false ; 
        _currentSource = null;
        LaserLineReceiver = null;
    }

    public virtual void StartReturnable(LaserMachine laser, RaycastHit hit)
    {
        _triggerByLaser = true; 
        _currentSource = laser ;
        LaserLineReceiver = laser.LaserLine;
        Returnable(laser, hit);
        LaserLine.enabled = true;
        LaserLine.SetPosition(1, hit.point); 
        LaserLine.SetPosition(0, hit.point); 

    }
}
