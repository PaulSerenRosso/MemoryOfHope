using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ListenerActivate : ListenerTrigger
{
  
    [SerializeField] private UnityEvent ActivateEvent;
    [SerializeField] private string message;
    [SerializeField] private bool _destroyWhenActivate;

    public override void Raise()
    {
        PlayerManager.instance.CurrentListenerActivate = this;
        base.Raise();
    }

    public override void EndRaise()
    {
        PlayerManager.instance.CurrentListenerActivate = null;
        base.EndRaise();
    }

    public virtual void Activate()
    {
        ActivateEvent?.Invoke();
        
     
    }
}