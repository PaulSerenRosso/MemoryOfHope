using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DoorLaser : MonoBehaviour, IReturnable
{
    [SerializeField]
    private MeshRenderer _meshRenderer;
    [SerializeField]
    private Collider _collider;

    [SerializeField] private UnityEvent _activeEvent;
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
    [SerializeField]
    private AudioSource _audioSource;

    public void StartReturnableFeedBack()
    {
        _activeEvent?.Invoke();
    }

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
        /*
        if (IsActive)
        {
        _meshRenderer.enabled = true;
        _collider.enabled = true;
        }
        _triggerByLaser = false;
        _currentSource = null;*/
    }

    public void StartReturnable(LaserMachine laser, RaycastHit hit)
    {
        if (IsActive)
        {
        _meshRenderer.enabled = false;
        _collider.enabled = false;
        _audioSource.enabled = false; 
        }
        _triggerByLaser = true;
        _currentSource = laser;
    }
}