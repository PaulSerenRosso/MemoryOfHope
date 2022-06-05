using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ListenerTrigger : MonoBehaviour
{
    [SerializeField] protected UnityEvent Listeners;
    [SerializeField] protected UnityEvent _endTriggerListeners;


 
    [SerializeField] private Collider _collider;

    private bool _isActiveTrigger;

    public bool IsActiveTrigger
    {
        get { return _isActiveTrigger; }
        set
        {
            _isActiveTrigger = value;
            _collider.enabled = value;
        }
    }

    void Start()
    {
        if(_collider != null)
        _isActiveTrigger = _collider.enabled;
    }


    public virtual void Raise()
    {
        Listeners?.Invoke();
    }

    public virtual void EndRaise()
    {
        _endTriggerListeners?.Invoke();
    }

    public void ActivateTrigger() => IsActiveTrigger = true;

    public void DesactivateTrigger() => IsActiveTrigger = false;

    public void Destroy(float timer) => Destroy(gameObject, timer);
}