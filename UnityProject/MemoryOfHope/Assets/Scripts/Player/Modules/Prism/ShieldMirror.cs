using UnityEngine;
using UnityEngine.Events;

public class ShieldMirror : BaseMirror
{
    [Header("ShieldMirror Variables")] [SerializeField]
    private float _offsetDistanceBeginPosition;

    [SerializeField] private AudioSource _reloadLaserAudioSource;
    [SerializeField] private float Yoffset;
    [SerializeField] private ShieldManager _shield;

    [SerializeField] private Color laserColor;

    public bool prismObtained = false;

    public override void Returnable(LaserMachine laser, RaycastHit hit)
    {
        if (!_reloadLaserAudioSource.isPlaying)
        {
            _reloadLaserAudioSource.Play();
        }


        _shield.LaserCharge += Time.deltaTime * _shield.LaserChargeRegeneration;

        if (prismObtained)
        {
            PlayerController.instance.hopeCape.GetColor("Color_Hope");
            PlayerController.instance.hopeCape.SetColor("Color_Hope", laserColor);
        }

        _triggerByLaser = true;

        if (_shield.InputShield && !_shield.inputLaser)
        {
            LaserLineReceiver = laser.LaserLine;
            LaserLine.enabled = true;
            LaserLine.enabled = true;

            Direction = transform.forward;
            Vector3 upOffset = PlayerController.instance.transform.TransformPoint(Vector3.up * Yoffset);
            BeginLaser = new Vector3(transform.position.x, upOffset.y, transform.position.z) +
                         Direction * _offsetDistanceBeginPosition;
            LaserLineReceiver.SetPosition(0, hit.point);
        }
        else if (!_shield.InputShield && !_shield.inputLaser)
        {
            LaserLine.enabled = false;
            EndTrigger();
            BeginLaser = Vector3.zero;
            Direction = Vector3.zero;
            LaserLine.enabled = false;
        }


        // augmenter le truc
        // puis le laser source à coté 
        // 
    }

    public override void Cancel(LaserMachine laser)
    {
        EndTrigger();
        _triggerByLaser = false;
        _currentSource = null;
        LaserLineReceiver = null;
        BeginLaser = Vector3.zero;
        Direction = Vector3.zero;
        LaserLine.enabled = false;
        _reloadLaserAudioSource.Stop();
        if (prismObtained)
        {
            PlayerController.instance.hopeCape.GetColor("Color_Hope");
            PlayerController.instance.hopeCape.SetColor("Color_Hope", Color.black);
        }
    }

    public override void StartReturnable(LaserMachine laser, RaycastHit hit)
    {
        _triggerByLaser = true;
        _currentSource = laser;
    }
}