using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLaserMultiple : MonoBehaviour
{
    [SerializeField]
    private List<DoorActivator> _allActivators;
    [SerializeField]
    private MeshRenderer _meshRenderer;
    [SerializeField]
    private Collider _collider;

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
            _meshRenderer.enabled = false;
            _collider.enabled = false;
            IsActive = false;
        }
        
    }
    
}
