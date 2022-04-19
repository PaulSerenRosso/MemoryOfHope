using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMirror : BaseMirror
{
    [Header("ShieldMirror Variables")]
    [SerializeField] private float _offsetDistanceBeginPosition;

    [SerializeField] private float Yoffset;

    [SerializeField] private ShieldManager _shield;
    


    public override void Returnable(LaserMachine laser, RaycastHit hit)
    { 
        _shield.LaserCharge += Time.deltaTime * _shield.LaserChargeRegeneration;
             
             _triggerByLaser = true;   
        if (_shield.InputShield && !_shield.inputLaser)
        {
            LaserLineReceiver = laser.LaserLine;
            LaserLine.enabled = true;
            LaserLine.enabled = true;
              Direction = transform.forward;
                    Vector3 upOffset = PlayerController.instance.transform.TransformPoint(Vector3.up * Yoffset);
                    BeginLaser = new Vector3(transform.position.x, upOffset.y, transform.position.z) +Direction*_offsetDistanceBeginPosition;
                    LaserLineReceiver.SetPosition(0, hit.point);
                    Debug.Log("test");
        }
        else if (!_shield.InputShield && !_shield.inputLaser)
        {
                LaserLine.enabled = false;
                                EndTrigger();
                                BeginLaser = Vector3.zero;
                                Direction = Vector3.zero;
                                LaserLine.enabled = false;
        }
       
        
       
      
        // augmenter le truc
        // puis le laser source à coté 
        // 
        
    }
    
    public virtual void Cancel(LaserMachine laser)
    {
        _triggerByLaser = false ; 
        _currentSource = null;
        LaserLineReceiver = null;
    }

    public virtual void StartReturnable(LaserMachine laser, RaycastHit hit)
    {
        
        _triggerByLaser = true; 
        _currentSource = laser ;

    }
}
