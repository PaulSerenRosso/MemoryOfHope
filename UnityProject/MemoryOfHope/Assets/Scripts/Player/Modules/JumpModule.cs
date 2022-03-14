
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpModule : Module
{
    
    [SerializeField] private float gravityJump;
//velocity.y += Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
    [SerializeField]
    private float maxHeightJump;
    [SerializeField]
    private float speedJump;
    private float yStartPosition;
    private float yEndMinPosition;

    float currentSpeed = 0;
    [SerializeField] private MoveModule moveModule;
    [SerializeField]
    private float minHeightJump;
    [SerializeField]
    private float inputMaxTime;
    [SerializeField]
    private float inputMinTime;
    private float inputTimer;
    private AnimationCurve curveJumpSpeed;
   bool inExecute;
    private float yCurrentEndMaxPosition;
    private bool isRelease = true;
   
    public override void LinkModule()
    {
        Debug.Log("Linking Inputs for Jump Module");

        PlayerController.instance.playerActions.Player.Jump.performed += context => InputPressed(context);
        PlayerController.instance.playerActions.Player.Jump.canceled += context => InputReleased(context);
    }

  
        

    public override bool Conditions()
    {
        if (!base.Conditions()) return false;
        if ((PlayerController.instance.onGround  && isRelease )|| !PlayerController.instance.onGround && isPerformed)
            return true;
        
       
      
        return false;
    }

    public override void InputPressed(InputAction.CallbackContext ctx)
    {
        if(isPerformed ||PlayerController.instance.onGround  )
        inputPressed = true;
        
    }

    void Update()
    {
        if (inExecute )
        {
            if (PlayerController.instance.onGround && !isPerformed)
            {
                PlayerController.instance.playerAnimator.SetBool("jumpAir", true);
                yStartPosition = PlayerController.instance.playerRb.position.y;
                yEndMinPosition = yStartPosition + minHeightJump;
                PlayerController.instance.stuckGround = false;
                isPerformed = true;
                isRelease = false;
                Debug.Log("testsaaaa");
                PlayerController.instance.currentGravity = gravityJump;
           currentSpeed = Mathf.Sqrt(-2f *  -PlayerController.instance.currentGravity  * minHeightJump);;
         
                PlayerController.instance.currentVelocity += currentSpeed*Vector3.up;
                if (moveModule.inputPressed)
                {
                    PlayerController.instance.currentVelocity += moveModule.moveVector ;
                }
            }  
            Debug.Log("test");
            if(inputTimer < inputMaxTime) inputTimer += Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        if (isPerformed)
        {

           
            Debug.Log(yEndMinPosition );
            Debug.Log(PlayerController.instance.playerRb.position.y >= yCurrentEndMaxPosition);
            Debug.Log(yCurrentEndMaxPosition != 0 );
            
            if (inputTimer > inputMinTime  && yCurrentEndMaxPosition == 0 || (yCurrentEndMaxPosition != 0 && PlayerController.instance.playerRb.position.y <= yCurrentEndMaxPosition))
            {

              float factorTime = inputTimer / inputMaxTime;
                yCurrentEndMaxPosition = factorTime * (maxHeightJump + yStartPosition);
                if (yCurrentEndMaxPosition == 0)
                {
                    currentSpeed = PlayerController.instance.playerRb.velocity.y;
                }
                PlayerController.instance.currentVelocity += (currentSpeed-PlayerController.instance.playerRb.velocity.y) * Vector3.up;
            }
            
            else if(PlayerController.instance.playerRb.position.y >= yEndMinPosition  || (yCurrentEndMaxPosition != 0 && PlayerController.instance.playerRb.position.y >= yCurrentEndMaxPosition))
                     {
                         Debug.Log("testqq");
                     inputTimer = 0;
                     inExecute = false;
                     PlayerController.instance.playerAnimator.SetBool("jumpAir", false);
                     PlayerController.instance.currentGravity = PlayerController.instance.defaultGravity;
                     PlayerController.instance.stuckGround = true;
                     isPerformed = false;
                     yCurrentEndMaxPosition = 0;
                 }
             
        
        }
    }

    public override void InputReleased(InputAction.CallbackContext ctx)
    { isRelease = true;
        inputPressed = false;
        Release();
    }
    
    public override void Execute()
    {
      
      inExecute = true; 
    }

    public override void Release()
    {
        if (!isPerformed)
        {
            Debug.Log("testqdfqsfsqdfq");
            inputTimer = 0; 
        }
        inExecute = false;
    }
    
    
}
