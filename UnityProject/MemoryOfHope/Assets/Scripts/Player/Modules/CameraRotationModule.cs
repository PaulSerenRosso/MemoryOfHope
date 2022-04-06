using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotationModule : Module
{ 
    private Vector2 inputVector;
    public float minXAngle;
    public float maxXAngle;
    public float speedYAngle;
    public float speedXAngle;
    
  
    public override void LinkModule()
    {
        PlayerController.instance.playerActions.Player.RotateCamera.performed += context => InputPressed(context);
        PlayerController.instance.playerActions.Player.RotateCamera.canceled += context => InputReleased(context);
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

    public override void Execute()
    {
        isPerformed = true;
    }

    public override void Release()
    {
        
        isPerformed = false;
    }

    public void Update()
    {
        if(!isPerformed) return;
        Vector3 currentRotation = MainCameraController.Instance.transform.eulerAngles;
        MainCameraController.Instance.transform.rotation = Quaternion.Euler(currentRotation.x+ speedXAngle*Time.deltaTime*-inputVector.y, 
            speedYAngle*Time.deltaTime*inputVector.x+currentRotation.y, currentRotation.z);
        currentRotation = MainCameraController.Instance.transform.eulerAngles;
        currentRotation.x = Mathf.Clamp(currentRotation.x, minXAngle, maxXAngle);
        MainCameraController.Instance.transform.eulerAngles = currentRotation;
    }
}
