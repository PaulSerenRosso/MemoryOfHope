using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMachine : MonoBehaviour
{
    [Header("LaserMachine Variables")]
    public bool IsActive;
    protected bool _triggerByLaser;
    public LineRenderer LaserLine;
    public  Vector3 BeginLaser;
    public  Vector3 Direction;
  public float MaxDistance;
  public LayerMask RayLayer;
    public int IndexBeginLineRenderer;
    public int IndexEndLineRenderer;
    
    private RaycastHit _hit;
    private Ray _ray;
    private bool _isTrigger;
    private IReturnable _returnable;
    private GameObject _returnableObject;
 

    public virtual void FixedUpdate()
    {
        if (IsActive)
        {
               if (_triggerByLaser)
                             {
                                 if(LaserLine.GetPosition(IndexEndLineRenderer) == BeginLaser)
                                     LaserLine.enabled = true;
                                 _ray = new Ray(BeginLaser, Direction);
                                 LaserLine.SetPosition(IndexBeginLineRenderer, BeginLaser);
                                 Debug.DrawRay(_ray.origin, _ray.direction*MaxDistance, Color.green);
                                 if (Physics.Raycast(_ray, out _hit, MaxDistance, RayLayer))
                                 {
                                     Debug.DrawRay(_ray.origin, _ray.direction*MaxDistance, Color.red);
                                     _isTrigger = true;
                                     LaserLine.SetPosition(IndexEndLineRenderer, _hit.point);
                                     if (_returnableObject == _hit.collider.gameObject)
                                         TriggerSameObject();
                                     else
                                         TriggerNewObject();
                                 }
                                 else
                                 { LaserLine.SetPosition(IndexEndLineRenderer, _ray.origin+Direction*MaxDistance);
                                     if (_isTrigger) { EndTrigger();
                                     }
                                 }
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
        if (_returnable != null )
        {
            _returnable.Returnable(this, _hit);
        }
    }

    void TriggerNewObject()
    {
        _returnableObject = _hit.collider.gameObject;
        if (_returnable != null)
        {
            _returnable.Cancel(this);
        }
        if (_hit.transform.TryGetComponent(out IReturnable _currentReturnable) && !_currentReturnable.IsReturnLaser ) 
        {
            _returnable = _currentReturnable;
            _returnable.StartReturnable(this, _hit);
        }
        else
        {
            _returnable = null;
        }
    }

   protected void EndTrigger()
    {
        _isTrigger = false;
        if (_returnable != null)
        {
            _returnable.Cancel(this);
        }
        _returnable = null;
        _returnableObject = null;
    }
}
