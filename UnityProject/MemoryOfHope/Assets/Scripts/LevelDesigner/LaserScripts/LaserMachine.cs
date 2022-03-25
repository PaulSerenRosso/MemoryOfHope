using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMachine : MonoBehaviour
{
    [Header("LaserMachine Variables")] public bool IsActive;
    protected bool _triggerByLaser;
    public LineRenderer LaserLine;
    public Vector3 BeginLaser;
    public Vector3 Direction;
    public float MaxDistance;
    public LayerMask RayLayer;


    private RaycastHit _hit;
    private Ray _ray;
    private bool _isTrigger;
    private IReturnable _returnable;
    private GameObject _returnableObject;
    private bool checkIfStartReturnable;


    public virtual void FixedUpdate()
    {
  
        if (IsActive)
        {
            if (_triggerByLaser)
            {
                if (LaserLine.GetPosition(0) == BeginLaser)
                    LaserLine.enabled = true;
                _ray = new Ray(BeginLaser, Direction);

                Debug.DrawRay(_ray.origin, _ray.direction * MaxDistance, Color.green);
                if (Physics.Raycast(_ray, out _hit, MaxDistance, RayLayer))
                {
                    Debug.DrawRay(_ray.origin, _ray.direction * MaxDistance, Color.red);


                    if (_returnableObject == _hit.collider.gameObject)
                        TriggerSameObject();
                    else
                        TriggerNewObject();
                    _isTrigger = true;

                    LaserLine.SetPosition(1, _hit.point);
                }
                else
                {
                    LaserLine.SetPosition(1, _ray.origin + Direction * MaxDistance);
                    if (_isTrigger)
                    {
                        EndTrigger();
                    }
                }

                LaserLine.SetPosition(0, BeginLaser);
            }
            else
            {
                EndTrigger();
            }
        }
        else
        {
            EndTrigger();
        }
    }

    void TriggerSameObject()
    {
        if (_returnable != null)
        {
            if (_returnable.IsActiveReturnable)
            {
                if (!_returnable.IsReturnLaser && !checkIfStartReturnable)
                {
                    _returnable.StartReturnable(this, _hit);
                    checkIfStartReturnable = true;
                }
                else if (checkIfStartReturnable && _returnable.CurrentSource == this)
                {
                    _returnable.Returnable(this, _hit);
                }
            }
            else
            {
                if (_returnable != null && _returnable.CurrentSource == this)
                {
                    _returnable.Cancel(this);
                }

                checkIfStartReturnable = false;
            }
        }
    }

    void TriggerNewObject()
    {
        if (_returnable != null)
        {
            EndTrigger();
        }

        _returnableObject = _hit.collider.gameObject;

        if (_returnableObject.TryGetComponent(out IReturnable _currentReturnable))
        {
            _returnable = _currentReturnable;
           
            if (!_currentReturnable.IsReturnLaser && _currentReturnable.IsActiveReturnable)
            {
                _returnable.StartReturnable(this, _hit);
                checkIfStartReturnable = true;
            }
        }
        else
        {
          
            _returnable = null;
        }
    }

    protected void EndTrigger()
    {
        if (_returnable != null && _returnable.CurrentSource == this)
        {
            _returnable.Cancel(this);
        }

        _isTrigger = false;
        checkIfStartReturnable = false;
        _returnable = null;
        _returnableObject = null;
    }
}