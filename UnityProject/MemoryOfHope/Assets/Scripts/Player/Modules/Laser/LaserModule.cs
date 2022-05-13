using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaserModule : Module
{
    // savoir qu'elle touche c'est 
    //est ce que je peux utiliser mon laser en meme temps que de recevoir un laser
    // il se passe quoi quand j'ai plus de laser
    // ui comment je gère
    
    //ui 
    [SerializeField] private ShieldManager _shield;
    public override void Cancel()
    {
        Release();
    }

    public override bool Conditions()
    {
        if (!base.Conditions()){ Release(); return false;}

        if (PlayerManager.instance.isHit)
        {
            Release(); return false; 
        }

        if (_shield.LaserCharge < _shield.LaserChargeCost*Time.deltaTime) {Release(); return false;}
        if (!PlayerController.instance.onGround)
        {Release(); return false;} 
        
        
        return true;
    }
    public override void LinkModule()
    {
        GameManager.instance.inputs.Player.Laser.canceled += InputReleased;
        GameManager.instance.inputs.Player.Laser.performed += InputPressed;
        isLinked = true;
    }
    
    private void OnDisable()
    {
        UnlinkModule();
    }

    public override void UnlinkModule()
    {
        if (!isLinked) return;
        GameManager.instance.inputs.Player.Laser.canceled -= InputReleased;
        GameManager.instance.inputs.Player.Laser.performed -= InputPressed;
    }

    public override void InputPressed(InputAction.CallbackContext ctx)
    {
        inputPressed = true;
    }

    public override void InputReleased(InputAction.CallbackContext ctx)
    {
        inputPressed = false;
    }

    public override void Execute()
    {
        
        isPerformed = true;
        if (_shield.InputShield)
        {
        
            _shield.inputLaser = true;
            _shield.Laser.enabled = true;
                    _shield.Laser.IsActive = true;
                    _shield.LaserCharge -= _shield.LaserChargeCost * Time.deltaTime;
        }
        
        
    }

    public override void Release()
    {
     
        _shield.inputLaser = false;
        _shield.Laser.IsActive = false; 
        isPerformed = false;
  

    }



    
}

