using UnityEngine;
using UnityEngine.Events;


public class ShieldManager : MonoBehaviour
{
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private Collider _collider;

    public bool isTutorial;
    [SerializeField] private TutorialGameEvent reloadTuto;

    public float MaxLaserCharge;
    public float _laserCharge;

  //  [SerializeField] private AudioSource _reloadShieldAudioSource;
  
  //  [SerializeField] private UnityEvent _shieldDamageEvent;


    public float LaserCharge
    {
        get { return _laserCharge; }
        set
        {
            if (isTutorial && value > _laserCharge)
            {
                reloadTuto.RemoveTutorial();
                isTutorial = false;
            }
            _laserCharge = Mathf.Min(value, MaxLaserCharge);
            UIInstance.instance.LaserSlider.value = value;
        }
    }

    public float LaserChargeRegeneration;
    public float LaserChargeCost;

    private void OnValidate()
    {
       // _health = _maxHealth;
    }

    public LaserSource Laser;
    public ShieldMirror Mirror;
    public bool inputLaser;
    private bool _inputShield;

    public bool InputShield
    {
        get { return _inputShield; }
        set
        {
            _inputShield = value;
            
          //  if (!isDead)
            //{
                _mesh.enabled = value;
                _collider.enabled = value;
            //}
        }
    }

 //   [SerializeField] private int _health;
   // [SerializeField] private int _maxHealth;
    // [SerializeField] private bool _isDead;

   // [SerializeField] private float timeDeath;
    //private float timerDeath;

  /*  public int health
    {
        get { return _health; }
        set { _health = value; }
    }

    public int maxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    public bool isDead
    {
        get { return _isDead; }
        set { _isDead = value; }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        _shieldDamageEvent?.Invoke();
        if (health <= 0)
        {
            isDead = true;
            health = 0;
            Death();
        }
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth)
            health = maxHealth;
    }

    public void Death()
    {
        _mesh.enabled = false;
        Laser.IsActive = false;
        _collider.enabled = false;
    }
  
    private void Update()
    {
        
        if (isDead)
        {
            if (timeDeath > timerDeath)
            {
                if (!_reloadShieldAudioSource.isPlaying)
                   _reloadShieldAudioSource.Play();
                timerDeath += Time.deltaTime;
            }
            else
            {
                _isDead = false;
                timerDeath = 0;
                _reloadShieldAudioSource.Stop();
                if (_inputShield)
                {
                    _mesh.enabled = true;
                    _collider.enabled = true;
                }
                
                
                Heal(maxHealth);
            }
        }

        
    }
    */
}