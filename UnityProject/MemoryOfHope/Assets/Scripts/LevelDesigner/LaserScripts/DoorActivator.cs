using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActivator : MonoBehaviour, IReturnable
{
    [SerializeField] private DoorLaserMultiple currentDoor;
    public virtual bool IsReturnLaser
    {
        get { return _triggerByLaser; }
        set { _triggerByLaser = value; }
    }

    public bool IsActive;
    public bool IsActiveReturnable
    {
        get => IsActive;
        set { IsActive = value; }
    }

    public LaserMachine _currentSource;
    private bool _triggerByLaser;

    public LaserMachine CurrentSource
    {
        get => _currentSource;
        set => _currentSource = value;
    }
    public void Returnable(LaserMachine laser, RaycastHit hit)
    {
       
    }
    public void Cancel(LaserMachine laser)
    {
        if (currentDoor.IsActive)
        {
            _triggerByLaser = false;
            _currentSource = null;
        }
    }
    public void StartReturnable(LaserMachine laser, RaycastHit hit)
    {
        if (currentDoor.IsActive)
        {
            _triggerByLaser = true; 
        _currentSource = laser ;
        currentDoor.CheckActivator();
        }
        
    }
}
