using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenerTriggerTimer : ListenerTrigger
{
    [SerializeField] private EventTimer[] _eventTimers;
    private int _currentTimerIndex;
    private bool _timerIsActive;
    private float _currentTimer;

    public override void Raise()
    {
        base.Raise();
        
    }
    // Update is called once per frame
    void Update()
    {
        if (_timerIsActive)
        {
            _currentTimer += Time.deltaTime;
            
            if (_eventTimers[_currentTimerIndex].Time <= _currentTimer)
            {
                _eventTimers[_currentTimerIndex].Event?.Invoke();
                _currentTimerIndex++;
                if (_currentTimerIndex == _eventTimers.Length)
                {
                    DesactivateTimer();
                }
            }
        }
    }

    public void ActivateTimer()
    {        _currentTimer = 0; 
        _currentTimerIndex = 0; 
        _timerIsActive = true;
    }

    public void DesactivateTimer()
    {
        _timerIsActive = false; 
    }
}
