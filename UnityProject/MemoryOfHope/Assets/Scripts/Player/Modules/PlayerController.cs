using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Modules
    [Header("Modules")] public List<Module> activeModulesUpdate;
    public List<Module> activeModulesFixed;
    [SerializeField] private List<int> currentModuleUpdate;
    [SerializeField] private List<int> currentModuleFixed;
    #endregion

    #region PlayerComponent

    [Header("PlayerComponent")] public Rigidbody playerRb;
    public Animator playerAnimator;
    public InputMaster playerActions;

    #endregion
    
    #region Gravity

    [Header("Gravity")] [SerializeField] private float defaultGravity;
    public float currentGravity;

    #endregion

    #region Vectors for Velocity

    [Header("Vectors for Velocity")] public Vector3 currentVelocity;
    public Vector3 currentVelocityWithUndo;
    private Vector3 undoVelocity;
    private Vector3 finalVelocity;
    private Vector3 oldVelocity;
    private Vector3 physicsFactor;

    #endregion
    
    #region Detect Ground

    [Header("Detect Ground")] public float angleGround;
    public Vector3 currentNormalGround;
    private Collider currentGround;
    private Collider currentWall;
    private Vector3 currentNormalWall;
    public bool stuckGround = true;
    public bool onGround;
    private Vector3 projectionRB;
    private float alignedSpeed;

    #endregion

    #region Instance & Input

    public static PlayerController instance;

    private void Awake()
    {
        if (instance is { })
        {
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;

        playerActions = new InputMaster();
    }

    private void OnEnable()
    {
        playerActions.Enable();
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }

    #endregion

    #region Main Functions

    private void Start()
    {
        for (int i = 0; i < activeModulesUpdate.Count; i++)
        {
            Module module = activeModulesUpdate[i];
            module.LinkModule();
        }

        for (int i = 0; i < activeModulesFixed.Count; i++)
        {
            Module module = activeModulesFixed[i];
            module.LinkModule();
        }

        currentGravity = defaultGravity;
    }

    void Update()
    {
        CheckModuleUpdate();
        CheckCurrentModuleUpdate();
    }

    void FixedUpdate()
    {
        CheckModuleFixed();
        CheckCurrentModuleFixed();
        CalculateVelocity();
    }

    #endregion

    #region CalculateVelocity

    void CalculateVelocity()
    {
        finalVelocity = Vector3.zero;
        CalculateUndoVelocity();
        CalculateCurrentVelocity();
        CalculateUndoVelocity();
        CalculateVelocityWithUndo();
        CalculateGravity();
        CheckNormals();
    }

    void CalculateUndoVelocity()
    {
        if (undoVelocity != Vector3.zero)
        {
            physicsFactor = oldVelocity - playerRb.velocity;
            undoVelocity += physicsFactor;
            finalVelocity += undoVelocity;
            undoVelocity = Vector3.zero;
        }
    }

    void CalculateCurrentVelocity()
    {
        if (currentVelocity != Vector3.zero)
        {
            finalVelocity += currentVelocity;
            currentVelocity = Vector3.zero;
        }
    }
    void CalculateVelocityWithUndo()
    {
        if (currentVelocityWithUndo != Vector3.zero)
        {
            finalVelocity += currentVelocityWithUndo;
            undoVelocity += -currentVelocityWithUndo;
            currentVelocityWithUndo = Vector3.zero;
        }
    }
    void CalculateGravity()
    {
        if (!onGround)
        {
            if (currentGravity == 0)
                currentGravity = defaultGravity;
            finalVelocity += Vector3.down * currentGravity;
        }
    }

    void CheckNormals()
    {
        if (finalVelocity != Vector3.zero)
        {
            playerRb.velocity += finalVelocity;
             oldVelocity = playerRb.velocity;
            if (onGround && stuckGround)
            {
                projectionRB = Vector3.ProjectOnPlane(playerRb.velocity, currentNormalGround).normalized;
                alignedSpeed = Vector3.Dot(playerRb.velocity, projectionRB);
                projectionRB *= alignedSpeed;

                playerRb.velocity = projectionRB;
            }
            else if (!onGround && currentWall != null)
            {
                if (Vector3.Angle(playerRb.velocity, currentNormalWall) >= 90)
                {
                    projectionRB = Vector3.ProjectOnPlane(playerRb.velocity, currentNormalWall).normalized;
                    alignedSpeed = Vector3.Dot(playerRb.velocity, projectionRB);
                }
            }
        }
       
    }

    void CheckModuleUpdate()
    {
        for (int i = 0; i < activeModulesUpdate.Count; i++)
        {
            if (!activeModulesUpdate[i].Conditions())
                continue;
                currentModuleUpdate.Add(i);
        }
    }

    void CheckModuleFixed()
    {
        for (int i = 0; i < activeModulesFixed.Count; i++)
        {
            if (!activeModulesFixed[i].Conditions())
                continue;
            currentModuleFixed.Add(i);
        }
    }

    void CheckCurrentModuleUpdate()
    {
        if (currentModuleUpdate.Count == 0)
            return;

        for (int i = 0; i < currentModuleUpdate.Count; i++)
        {
            activeModulesUpdate[currentModuleUpdate[i]].Execute();
            
        }

        currentModuleUpdate.Clear();
    }

    void CheckCurrentModuleFixed()
    {
        if (currentModuleFixed.Count == 0)
            return;

        for (int i = 0; i < currentModuleFixed.Count; i++)
        {
            activeModulesFixed[currentModuleFixed[i]].Execute();
        }

        currentModuleFixed.Clear();
    }

    #endregion

    #region OnCollision

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Vector3 normal = other.GetContact(0).normal;
            if (normal.y >= angleGround)
            {
                if (!onGround)
                {
                    onGround = true;
                    currentGravity = 0;
                    playerAnimator.SetBool("onGround", true);
                }

                currentNormalGround = normal.normalized;
                currentGround = other.collider;
            }
            else
            {
                currentNormalWall = normal.normalized;
                currentWall = other.collider;
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (currentGround == other.collider)
            {
                onGround = false;
                currentGround = null;
                currentNormalGround = Vector3.zero;
                playerAnimator.SetBool("onGround", false);
            }
            else if (currentWall == other.collider)
            {
                currentWall = null;
                currentNormalWall = Vector3.zero;
            }
        }
    }

    #endregion
}