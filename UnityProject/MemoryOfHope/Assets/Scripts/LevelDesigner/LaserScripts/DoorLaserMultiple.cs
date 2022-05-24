using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorLaserMultiple : MonoBehaviour
{
   
    public List<DoorActivator> _allActivators;
    [SerializeField]
    private MeshRenderer _meshRenderer;

    [SerializeField]
    private UnityEvent updateActivatorCount;
    
    private int _activedActivatorCount;
    public int ActivedActivatorsCount
    {
        get
        {
            return _activedActivatorCount; 
        }
        set
        {
            updateActivatorCount?.Invoke();
            _activedActivatorCount = value;
        }
    }
    [SerializeField] private AudioSource _audioSource;
    [SerializeField]
    private Collider _collider;
    

    [SerializeField] private UnityEvent _activateEvent;
    public bool IsActive;

    public void CheckActivator()
    {
        for (int i = 0; i < _allActivators.Count; i++)
        {
            if (!_allActivators[i].IsReturnLaser)
                return;
        }
        if (IsActive)
        {
            _activateEvent?.Invoke();
            _meshRenderer.enabled = false;
            _collider.enabled = false;
            _audioSource.enabled = false; 
            IsActive = false;
        }
        
    }
    
}
