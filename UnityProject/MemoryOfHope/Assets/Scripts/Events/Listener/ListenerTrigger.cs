using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ListenerTrigger : MonoBehaviour
{
    [SerializeField]
    protected UnityEvent Listeners;
    [SerializeField]
    protected bool _destroyWhenTrigger;
    [SerializeField]
    protected float _timeToDestroy;

    public virtual void Raise()
    {
        Listeners?.Invoke();
        if(_destroyWhenTrigger)
           Destroy(gameObject, _timeToDestroy);
    }
}
