using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ListenerTriggerStay : MonoBehaviour
{
    [SerializeField] protected UnityEvent UpdateListeners;
    [SerializeField] protected UnityEvent StartListeners;

    [SerializeField] protected float _timeToDestroy;
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
        _isActiveTrigger = _collider.enabled;
    }

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
    }

    public void ActivateTrigger() => IsActiveTrigger = true;

    public void DesactivateTrigger() => IsActiveTrigger = false;

    public void Destroy(float timer) => Destroy(gameObject, timer);
}