using UnityEngine;
using UnityEngine.InputSystem;


public class MoveModule : Module
{
    private Vector2 inputVector;
    public Vector3 moveVector;
    [SerializeField] private float defaultSpeedMovment;
    [SerializeField] private float defaultSpeedRotationOppositeRun;
    [SerializeField] private float defaultSpeedRotation;
    [SerializeField] private float toleranceRotation;
    [SerializeField] private float factorAngleOppositeRun;
    private bool canMove;
    [SerializeField] private float airSpeedMovment;
    [SerializeField] private float airSpeedRotationOppositeRun;
    [SerializeField] private float airSpeedRotation;


    public Vector3 maxAirVelocity;
    public override void LinkModule()
    {
       

        PlayerController.instance.playerActions.Player.Move.performed += context => InputPressed(context);
        PlayerController.instance.playerActions.Player.Move.canceled += context => InputReleased(context);
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
        if (!isPerformed)
        {
            isPerformed = true;
        }

        Vector2 angleFoward = new Vector2(transform.forward.x,
            transform.forward.z);
        float differenceDir = (angleFoward - inputVector.normalized).magnitude;


        if (factorAngleOppositeRun < differenceDir)
        {
            canMove = false;
        }

        if (!canMove && differenceDir < toleranceRotation)
            canMove = true;
        if (canMove)
        {
            float currentSpeed;
            float currentRotationSpeed;
            moveVector.x = inputVector.x;
            moveVector.z = inputVector.y;
            if (PlayerController.instance.onGround)
            {
                currentRotationSpeed = defaultSpeedRotation;
                currentSpeed = defaultSpeedMovment;
            }
            else
            {
                currentRotationSpeed = airSpeedRotation;
                currentSpeed = airSpeedMovment;
                float RbmagnitudeXZ= new Vector3(PlayerController.instance.playerRb.velocity.x, 0, PlayerController.instance.playerRb.velocity.z).magnitude;    
                float maxAirVelocityMagnitude = maxAirVelocity.magnitude;
                if (RbmagnitudeXZ + currentSpeed > maxAirVelocityMagnitude)
                {
                    currentSpeed = maxAirVelocityMagnitude - RbmagnitudeXZ;
                }
            }

            moveVector *= currentSpeed;
            if (!PlayerController.instance.onGround)
            {
                PlayerController.instance.currentVelocity += moveVector;
            }
            else
            {
                 PlayerController.instance.currentVelocityWithUndo += moveVector;
            }
            
           
            Vector2 rotationVector = Vector3.RotateTowards(angleFoward, inputVector, currentRotationSpeed * 2, 00f);
            PlayerController.instance.playerRb.rotation =
                Quaternion.Euler(Vector3.up * Mathf.Atan2(rotationVector.x, rotationVector.y) * Mathf.Rad2Deg);
            PlayerController.instance.playerAnimator.SetFloat("movmentSpeed", inputVector.magnitude);
        }
        else
        {
            float currentOppositeRotationSpeed;
            if (PlayerController.instance.onGround)
                currentOppositeRotationSpeed = defaultSpeedRotationOppositeRun;
            else
                currentOppositeRotationSpeed = airSpeedRotationOppositeRun;
            Vector2 rotationVector = Vector3.RotateTowards(angleFoward, inputVector, currentOppositeRotationSpeed, 00f);
            PlayerController.instance.playerRb.rotation =
                Quaternion.Euler(Vector3.up * Mathf.Atan2(rotationVector.x, rotationVector.y) * Mathf.Rad2Deg);
            PlayerController.instance.playerAnimator.SetFloat("movmentSpeed", 0);
        }
    }

    public override void Release()
    {
        Debug.Log("release");
        isPerformed = false;
        PlayerController.instance.playerAnimator.SetFloat("movmentSpeed", 0);
    }
}