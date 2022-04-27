using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ListenerTriggerInput : ListenerTriggerStay
{
    [SerializeField]
    public Module _module;
    [SerializeField]
    private UnityEvent _inputEvent;
    [SerializeField]
    protected bool _destroyWhenInput;
    [SerializeField]
    protected float _timeToDestroyInput;
    public override void Raise()
    {
        base.Raise();
        if (_module.isPerformed)
        {
            RaiseInputEvent();
        }
    }

    public void RaiseInputEvent()
    {
        _inputEvent?.Invoke();
        if(_destroyWhenInput)
            Destroy(gameObject, _timeToDestroyInput);
    }
}
