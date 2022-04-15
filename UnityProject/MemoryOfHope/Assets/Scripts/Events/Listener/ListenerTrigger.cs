using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ListenerTrigger : MonoBehaviour
{
    [SerializeField] protected UnityEvent Listeners;
    [SerializeField] protected UnityEvent _endTriggerListeners;
    [SerializeField] protected bool _destroyWhenTrigger;
    [SerializeField] protected float _timeToDestroyWhenTrigger;

    [SerializeField] protected bool _destroyWhenEndTrigger;
    [SerializeField] protected float _timeToDestroyWhenEndTrigger;

    public virtual void Raise()
    {
        Listeners?.Invoke();
        if (_destroyWhenTrigger)
            Destroy(gameObject, _timeToDestroyWhenTrigger);
    }

    public virtual void EndRaise()
    {
        _endTriggerListeners?.Invoke();
        if (_destroyWhenEndTrigger)
            Destroy(gameObject, _timeToDestroyWhenEndTrigger);
    }
}