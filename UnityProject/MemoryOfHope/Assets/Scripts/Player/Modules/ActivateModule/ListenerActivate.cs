using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ListenerActivate : ListenerTrigger
{
    public bool isActivate;
    [SerializeField]
    private UnityEvent ActivateEvent;
    [SerializeField]
    private string message;
[SerializeField]
    private bool _destroyWhenActivate;
    public override void Raise()
 {
     if(isActivate) return;
     UIInstance.instance.SetNotification(message, true);
     Debug.Log("tesfdqsft");
     PlayerManager.instance.CurrentListenerActivate = this; 
     base.Raise();
 }

 public override void EndRaise()
 {
     if(isActivate) return;
     UIInstance.instance.SetNotification(null, false);
     PlayerManager.instance.CurrentListenerActivate = null; 
     Debug.Log("tesfdqsftdfdfsfdfqsqs");
     base.EndRaise();
 }

 public virtual void Activate()
 {
     UIInstance.instance.SetNotification(null, false);
 ActivateEvent?.Invoke();
 PlayerManager.instance.CurrentListenerActivate = null;
 if(_destroyWhenActivate)
 Destroy(gameObject, Time.deltaTime);
 }
}
