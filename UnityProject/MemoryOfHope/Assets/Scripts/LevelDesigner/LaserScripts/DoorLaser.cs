using UnityEngine;
using UnityEngine.Events;

public class DoorLaser : MonoBehaviour, IReturnable
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Collider _collider;

    [SerializeField] private ParticleSystem breakDoorParticleSystem;

    [SerializeField] private UnityEvent _activeEvent;

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
    [SerializeField] private AudioSource _audioSource;

    public void StartReturnableFeedBack()
    {
        _activeEvent?.Invoke();
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
    }

    public void StartReturnable(LaserMachine laser, RaycastHit hit)
    {
        if (IsActive)
        {
            _meshRenderer.enabled = false;
            _collider.enabled = false;
            _audioSource.enabled = false;
            breakDoorParticleSystem.Play();
        }

        _triggerByLaser = true;
        _currentSource = laser;
    }
}