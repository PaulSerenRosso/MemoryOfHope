using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ListenerTriggerStay : MonoBehaviour
{
    [SerializeField]
    protected UnityEvent UpdateListeners;
    [SerializeField]
    protected UnityEvent StartListeners;
    [SerializeField]
    protected bool _destroyWhenTrigger;
    [SerializeField]
    protected float _timeToDestroy;

    public virtual void FirstRaise()
    {
        StartListeners?.Invoke();
    }
    public virtual void Raise()
    {
        UpdateListeners?.Invoke();
    }
    public virtual void EndRaise()
    {
        if(_destroyWhenTrigger)
        Destroy(gameObject, _timeToDestroy);
    }
    
}
