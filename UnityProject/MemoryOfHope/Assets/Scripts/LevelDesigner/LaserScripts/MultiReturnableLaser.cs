using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiReturnableLaser : MonoBehaviour,IReturnable
{
    [SerializeField]
    private List<LaserSource> _allExits;
    public LineRenderer LaserLineReceiver;
    
    public  void Cancel(LaserMachine laser)
    {
        _triggerByLaser = false; 
        _currentSource = null;
        LaserLineReceiver = null;
        for (int i = 0; i < _allExits.Count ; i++)
        {
            _allExits[i].IsActive = false;
        }
    }

    public bool IsActive;
    private bool _triggerByLaser;
    private LaserMachine _currentSource;
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
    public LaserMachine CurrentSource { get=>_currentSource; set => _currentSource = value; }

    public  void Returnable(LaserMachine laser, RaycastHit hit)
    {
        LaserLineReceiver.SetPosition(0, hit.point);
    }

    public void StartReturnable(LaserMachine laser, RaycastHit hit)
    {
        _triggerByLaser = true; 
        _currentSource = laser ;
        LaserLineReceiver = laser.LaserLine;
        Returnable(laser, hit);
        for (int i = 0; i < _allExits.Count ; i++)
        {
            _allExits[i].IsActive = true;
        }
    }



}
