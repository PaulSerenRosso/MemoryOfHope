using UnityEngine;
using UnityEngine.Events;

public class DoorActivator : MonoBehaviour, IReturnable
{
    [SerializeField] private DoorLaserMultiple currentDoor;

    
  public UnityEvent ActiveEvent;
  public UnityEvent DeactiveEvent;
    public virtual bool IsReturnLaser
    {
        get { return _triggerByLaser; }
        set { _triggerByLaser = value; }
    }

    public bool IsActive;

    public bool IsActiveReturnable
    {
        get => IsActive;
        set { IsActive = value; }
    }

    public LaserMachine _currentSource;
    private bool _triggerByLaser;

    public void StartReturnableFeedBack()
    {
        ActiveEvent?.Invoke();
    }

    public LaserMachine CurrentSource
    {
        get => _currentSource;
        set => _currentSource = value;
    }

    public void Returnable(LaserMachine laser, RaycastHit hit)
    {
    }

    public void Cancel(LaserMachine laser)
    {
        if (currentDoor.IsActive)
        {
            currentDoor.ActivedActivatorsCount--;
            DeactiveEvent?.Invoke();
            _triggerByLaser = false;
            _currentSource = null;
        }
    }

    public void StartReturnable(LaserMachine laser, RaycastHit hit)
    {
        if (currentDoor.IsActive)
        {
            currentDoor.ActivedActivatorsCount++;
            _triggerByLaser = true;
            _currentSource = laser;
            currentDoor.CheckActivator();
        }
    }
}