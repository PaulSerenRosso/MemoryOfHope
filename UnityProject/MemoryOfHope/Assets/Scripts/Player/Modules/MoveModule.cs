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


    public float maxAirSpeed;

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

        moveVector.x = inputVector.x;
        moveVector.z = inputVector.y;
        if (PlayerController.instance.onGround)
        {
            Vector2 angleForward = new Vector2(transform.forward.x,
                transform.forward.z);
            float differenceDir = (angleForward - inputVector.normalized).magnitude;
            if (factorAngleOppositeRun < differenceDir)
            {
                canMove = false;
            }

            if (!canMove && differenceDir < toleranceRotation)
                canMove = true;
            if (canMove)
            {
                MoveGround(angleForward);
            }
            else
            {
                Rotate(angleForward);
            }
        }
        else
        {
            MoveAir();
        }
    }

    public override void Release()
    {
        isPerformed = false;
        PlayerController.instance.playerAnimator.SetFloat("movmentSpeed", 0);
    }

    void MoveGround(Vector3 angleForward)
    {
        moveVector *= defaultSpeedMovment;
        PlayerController.instance.currentVelocityWithUndo += moveVector;
        Vector2 rotationVector = Vector3.RotateTowards(angleForward, inputVector, defaultSpeedRotation * 2, 00f);
        PlayerController.instance.playerRb.rotation =
            Quaternion.Euler(Vector3.up * Mathf.Atan2(rotationVector.x, rotationVector.y) * Mathf.Rad2Deg);
        PlayerController.instance.playerAnimator.SetFloat("movmentSpeed", inputVector.magnitude);
    }

    void MoveAir()
    {
        moveVector *= airSpeedMovment;
        ;
        Vector3 rbVelocityXZ =
            Vector3.ClampMagnitude(
                moveVector + new Vector3(PlayerController.instance.playerRb.velocity.x, 0,
                    PlayerController.instance.playerRb.velocity.z), maxAirSpeed);
        PlayerController.instance.playerRb.velocity = new Vector3(rbVelocityXZ.x,
            PlayerController.instance.playerRb.velocity.y, rbVelocityXZ.z);
    }

    void Rotate(Vector3 angleForward)
    {
        float currentOppositeRotationSpeed;
        currentOppositeRotationSpeed = defaultSpeedRotationOppositeRun;
        Vector2 rotationVector = Vector3.RotateTowards(angleForward, inputVector, currentOppositeRotationSpeed, 00f);
        PlayerController.instance.playerRb.rotation =
            Quaternion.Euler(Vector3.up * Mathf.Atan2(rotationVector.x, rotationVector.y) * Mathf.Rad2Deg);
        PlayerController.instance.playerAnimator.SetFloat("movmentSpeed", 0);
    }
}