using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveModule : Module
{
    public Vector2 inputVector;
    public Vector3 moveVector;
   public float defaultSpeedMovment;

   private float _blendAnimationFactor;
   [SerializeField]
   private float _blendAnimationSpeed;
    [SerializeField] private float defaultSpeedRotation;


    [SerializeField]
    private GameObject _fxDust;


    private Vector3 currentRotation;
    private AudioClip _runSound; 
    private bool canMove;
    [SerializeField] private float airSpeedMovment;
    private Vector2 inputCam;

    public float maxAirSpeed;

    public override void LinkModule()
    {
        GameManager.instance.inputs.Player.Move.performed += InputPressed;
        GameManager.instance.inputs.Player.Move.canceled += InputReleased;
        isLinked = true;
    }

    private void OnDisable()
    {
        UnlinkModule();
    }

    public override void UnlinkModule()
    {
        if (!isLinked) return;
        GameManager.instance.inputs.Player.Move.performed -= InputPressed;
        GameManager.instance.inputs.Player.Move.canceled -= InputReleased;
    }
    
    public override void InputPressed(InputAction.CallbackContext ctx)
    {
        inputPressed = true;
        inputVector = ctx.ReadValue<Vector2>();
    }

    public override void InputReleased(InputAction.CallbackContext ctx)
    {
        inputPressed = false;
        Release();
    }

    public override void Cancel()
    {
        if(isPerformed)
        Release();
    }

    public override bool Conditions()
    {
        if (!base.Conditions())
        {
            return false; 
        }

        if (PlayerManager.instance.isHit)
        {
            return false; // Ne peut pas bouger s'il est knockback
        }
        
        return true;
    }
    
    public override void Execute()
    {
        if (!isPerformed)
        {
            isPerformed = true;
        }

        Vector2 _cameraForwardXZ;
        Vector2 _cameraRightXZ;
        _cameraForwardXZ = new Vector3(MainCameraController.Instance.transform.forward.x,
            MainCameraController.Instance.transform.forward.z).normalized;
        _cameraRightXZ = new Vector3(MainCameraController.Instance.transform.right.x, 
            MainCameraController.Instance.transform.right.z).normalized;
        inputCam = _cameraForwardXZ * inputVector.y +
                   _cameraRightXZ * inputVector.x;
   
       canMove = true;
        
        moveVector.x = inputCam.x;
        moveVector.z = inputCam.y;
        if (PlayerController.instance.onGround)
        {
            Vector2 angleForward = new Vector2(transform.forward.x,
                transform.forward.z);
            
                MoveGround(angleForward);

        }
        else
        {
            if(PlayerManager.instance.MainAudioSource.isPlaying)
                PlayerManager.instance.MainAudioSource.Stop();
            MoveAir();
        }
    }

    private void Update()
    {
        if (isPerformed)
        {
            if (_blendAnimationFactor < inputCam.magnitude)
            {
                _blendAnimationFactor += _blendAnimationSpeed * Time.deltaTime;
                PlayerController.instance.playerAnimator.SetFloat("movmentSpeed", _blendAnimationFactor);
            }
        }
        else
        {
            if (_blendAnimationFactor > 0)
            {
                _blendAnimationFactor -= _blendAnimationSpeed * Time.deltaTime;
                PlayerController.instance.playerAnimator.SetFloat("movmentSpeed", _blendAnimationFactor);
            }
        }
    }

    public override void Release()
    {
    
        
        if(PlayerManager.instance.MainAudioSource.isPlaying)
            PlayerManager.instance.MainAudioSource.Stop();
        isPerformed = false;
        _fxDust.SetActive(false);
    }

    void MoveGround(Vector3 angleForward)
    {
        if (!PlayerManager.instance.MainAudioSource.isPlaying)
        {
            PlayerManager.instance.MainAudioSource.clip = _runSound;
            PlayerManager.instance.MainAudioSource.Play();
        }
        if(!_fxDust.activeSelf)
        _fxDust.SetActive(true);
        moveVector *= defaultSpeedMovment;
        PlayerController.instance.currentVelocityWithUndo += moveVector;
        Vector2 rotationVector = Vector3.SmoothDamp(angleForward, inputCam, ref currentRotation, defaultSpeedRotation );
    
        PlayerController.instance.playerRb.rotation =
            Quaternion.Euler(Vector3.up * Mathf.Atan2(rotationVector.x, rotationVector.y) * Mathf.Rad2Deg);
      
        
    }

    void MoveAir()
    {
        if(_fxDust.activeSelf)
        _fxDust.SetActive(false);
        moveVector *= airSpeedMovment;
        Vector3 rbVelocityXZ =
            Vector3.ClampMagnitude(
                moveVector + new Vector3(PlayerController.instance.playerRb.velocity.x, 0,
                    PlayerController.instance.playerRb.velocity.z), maxAirSpeed);
        PlayerController.instance.playerRb.velocity = new Vector3(rbVelocityXZ.x,
            PlayerController.instance.playerRb.velocity.y, rbVelocityXZ.z);
    }
    
}