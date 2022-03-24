using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_LaserContact : MonoBehaviour, IReturnable
{
    [SerializeField] private S_StateMachine _stateMachine;
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
        Debug.Log("teststststst");
       _stateMachine.OnHitByLaser();
    }

    public void Cancel(LaserMachine laser)
    {
        _triggerByLaser = false;
        _currentSource = null;
        _stateMachine.CancelHitLaser();
    }

    public void StartReturnable(LaserMachine laser, RaycastHit hit)
    {
        
        _currentSource = laser;
        _triggerByLaser = true;

    }
}
