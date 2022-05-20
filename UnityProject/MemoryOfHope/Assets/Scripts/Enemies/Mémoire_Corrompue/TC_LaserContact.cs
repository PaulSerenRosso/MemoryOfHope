using UnityEngine;

public class TC_LaserContact : MonoBehaviour, IReturnable
{
    [SerializeField] private TC_StateMachine _stateMachine;
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
        
    }

    public LaserMachine CurrentSource
    {
        get => _currentSource;
        set => _currentSource = value;
    }
    public void Returnable(LaserMachine laser, RaycastHit hit)
    {
        _stateMachine.OnHitByLaser();
    }

    public void Cancel(LaserMachine laser)
    {
        _triggerByLaser = false;
        _currentSource = null;
        _stateMachine.CancelHitLaser();
    }

    public void StartReturnable(LaserMachine laser, RaycastHit hit)
    {
        _currentSource = laser;
        _triggerByLaser = true;
    }
}
