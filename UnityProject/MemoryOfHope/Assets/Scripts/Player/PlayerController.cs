using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    #region Modules

    [Header("Modules")] public List<Module> activeModulesUpdate;
    public List<Module> activeModulesFixed;
    [SerializeField] private List<int> currentModuleUpdate;
    [SerializeField] private List<int> currentModuleFixed;



    [SerializeField] private List<Module> _movmentModule;

    #endregion

    #region PlayerComponent

    [Header("PlayerComponent")] public Rigidbody playerRb;
    public Animator playerAnimator;
    public Material hopeCape;
    public Color glitchColor;

    public bool isGlitching;
    public AttackModule attackModule;

    #endregion

    #region Gravity

    [Header("Gravity")] [SerializeField] public float defaultGravity;
    public float currentGravity;
    private bool _useGravity;

    public bool useGravity
    {
        get { return _useGravity; }
        set
        {
            _useGravity = value;
            if (value)
            {
                if (!onGround)
                {
                    currentGravity = defaultGravity;
                }
            }
        }
    }

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
    public bool IsProjectWallVelocity = true;
    bool _inMoveGround;
    private Vector3 _previousPositionMoveGround;
    public Vector3 _currentPositionMoveGround;
    private Vector3 _velocityMoveGround;
    private bool _previousPositionSet;
    private Vector3 _playerlocalPositionMoveGround;
    [SerializeField] private List<Module> _rotationModuleMovePlateform;
    [SerializeField] private MoveModule _moveModule;
    bool _isPerformedRotationModuleMovePlateform = false;
    Transform _moveGround;
    private Vector3 _playerLocalRotationMovePlateform;
    private bool _useCheckGround;
    private bool _playerLocalRotationMovePlateformIsSet;


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
        /*if (instance is { })
        {
            DestroyImmediate(gameObject);
            return;
        }*/

        instance = this;
    }

    private void OnEnable()
    {
        GameManager.instance.inputs.Player.Enable();
    }


    private void OnDisable()
    {
        GameManager.instance.inputs.Player.Disable();
        hopeCape.GetColor("Color_Hope");
        hopeCape.SetColor("Color_Hope", Color.black);
    }

    #endregion

    #region Main Functions

    private void Start()
    {
        GameManager.instance.inputs.Player.Enable();

        useGravity = true;
    }

    void Update()
    {
        if (PlayerManager.instance.IsActive)
        {
            CheckModuleUpdate();
            CheckCurrentModuleUpdate();
        }

        // CheckNearestObject();
    }

    void FixedUpdate()
    {
        if (PlayerManager.instance.IsActive)
        {
            CheckModuleFixed();
            CheckCurrentModuleFixed();
        }

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
        CalculateMovmentGround();
        CalculateVelocityWithUndo();
        CalculateGravity();
        CheckNormals();
    }

    void CalculateUndoVelocity()
    {
        physicsFactor = oldVelocity - playerRb.velocity;
        if (physicsFactor.magnitude <= undoVelocity.magnitude)
        {
            undoVelocity += physicsFactor;
            finalVelocity += undoVelocity;
        }

        undoVelocity = Vector3.zero;
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
        if (!onGround && useGravity)
        {
            playerRb.AddForce(Vector3.down * currentGravity, ForceMode.Acceleration);
        }
    }

    void CalculateMovmentGround()
    {
        if (!_inMoveGround) return;

        _isPerformedRotationModuleMovePlateform = false;
        if (_playerLocalRotationMovePlateformIsSet)
        {  
            transform.forward = _moveGround.TransformDirection(_playerLocalRotationMovePlateform);
        }
        if (!_moveModule.inputPressed)
        {
            for (int i = 0; i < _rotationModuleMovePlateform.Count; i++)
            {
                if (_rotationModuleMovePlateform[i].isPerformed)
                {
                    _isPerformedRotationModuleMovePlateform = true;
                  
                    break;
                }
            }

            if (!_isPerformedRotationModuleMovePlateform)
            {
                _playerLocalRotationMovePlateform = _moveGround.InverseTransformDirection(transform.forward);
                _playerLocalRotationMovePlateformIsSet = true; 
            }
            else
            {
                _playerLocalRotationMovePlateformIsSet = false;
            }
        }
        else
        {
            _playerLocalRotationMovePlateformIsSet = false;
        }

        if (_previousPositionSet)
        {
            _velocityMoveGround = _moveGround.transform.TransformPoint(_playerlocalPositionMoveGround) -
                                  _currentPositionMoveGround;
            ;
            _currentPositionMoveGround = playerRb.position;
            _playerlocalPositionMoveGround = _moveGround.InverseTransformPoint(_currentPositionMoveGround);
            _velocityMoveGround /= Time.deltaTime;
            currentVelocityWithUndo += _velocityMoveGround;
        }

        else
        {
            _currentPositionMoveGround = playerRb.position;
            _previousPositionSet = true;
            _velocityMoveGround = Vector3.zero;
            _playerlocalPositionMoveGround = _moveGround.InverseTransformPoint(_currentPositionMoveGround);
        }
    }

   
    public void SetMoveGround(Transform moveGround)
    {
        _moveGround = moveGround;
        _inMoveGround = true;
        _playerLocalRotationMovePlateformIsSet = false; 
        _previousPositionSet = false;
    }

    public void ResetMoveGround()
    {
        _inMoveGround = false;
    }


    void CheckNormals()
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
        else if (!onGround && currentWall != null && IsProjectWallVelocity)
        {
            if (Vector3.Angle(playerRb.velocity, currentNormalWall) >= 90)
            {
                projectionRB = Vector3.ProjectOnPlane(playerRb.velocity, currentNormalWall).normalized;
                
                alignedSpeed = Vector3.Dot(playerRb.velocity, projectionRB);
              
                projectionRB *= alignedSpeed;

                
                playerRb.velocity = projectionRB;
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
        if (!other.gameObject.CompareTag("Ground")) return;
        var points = new ContactPoint[10];
        other.GetContacts(points);

        var _isGround = false;
        var _isWall = false;
        for (int i = 0; i < points.Length; i++)
        {
            Vector3 normal = points[i].normal;

            if (normal.y >= angleGround)
            {
                _isGround = true;
                currentNormalGround = normal.normalized;
            }

            if (normal.y <= angleGround && normal != Vector3.zero)
            {
                _isWall = true;
                currentNormalWall = normal.normalized;
            }
        }

        if (!_isWall && currentWall == other.collider)
        {
            currentWall = null;
            currentNormalWall = Vector3.zero;
        }
        else if (_isWall)
            currentWall = other.collider;

        if (!_isGround && currentGround == other.collider)
        {
            onGround = false;
            currentGround = null;
            currentNormalGround = Vector3.zero;
            playerAnimator.SetBool("onGround", false);
            currentGravity = defaultGravity;
        }
        else if (_isGround)
        {
            if (!onGround)
            {
                onGround = true;
                playerAnimator.SetBool("onGround", true);
                playerRb.velocity = Vector3.zero;
                currentGravity = 0;
            }

            currentGround = other.collider;
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
                currentGravity = defaultGravity;
            }

            if (currentWall == other.collider)
            {
                currentWall = null;
                currentNormalWall = Vector3.zero;
            }
        }
        if (other.gameObject.CompareTag("EnemyBlocker"))
        {
       
            playerRb.velocity = Vector3.zero;
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        // Ajoute l'objet a une liste : les objets avec lesquels on peut interagir
        if (other.CompareTag("Interactible") && !interactiveObjects.Contains(other.transform))
        {
            interactiveObjects.Add(other.transform);
        }
    }

    private void CheckNearestObject() // Check tous les objets interactibles et renvoie le plus proche
    {
        if (interactiveObjects.Count != 0)
        {
            distances.Clear();
            for (int i = 0; i < interactiveObjects.Count; i++)
            {
                Transform obj = interactiveObjects[i];
                float distance = Vector3.Distance(transform.position, obj.position);
                distances.Add(distance);
                distances.Sort();
                if (Math.Abs(distances[0] - distance) < 0.001f)
                {
                    nearestObject = obj.gameObject;
                }
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
    
        // Retire l'objet de la liste d'objets avec lesquels on peut interagir
        if (other.CompareTag("Interactible") && interactiveObjects.Contains(other.transform))
        {
            if (nearestObject == other.gameObject)
            {
                nearestObject = null;
            }
            interactiveObjects.Remove(other.transform);
        }
    }
 */

    #endregion

    public Vector3 PlayerProjectOnPlane(Vector3 toProject)
    {
        if (Vector3.Angle(toProject, currentNormalWall) <= 90)
        {
            projectionRB = Vector3.ProjectOnPlane(toProject, currentNormalGround).normalized;
            alignedSpeed = Vector3.Dot(toProject, projectionRB);
            projectionRB *= alignedSpeed;
            return projectionRB;
        }

        return toProject;
    }

    public void CancelAllModules()
    {
        for (int i = 0; i < activeModulesFixed.Count; i++)
        {
            if (activeModulesFixed[i].isPerformed)
            {
                activeModulesFixed[i].Cancel();
            }
        }

        for (int i = 0; i < activeModulesUpdate.Count; i++)
        {
            if (activeModulesUpdate[i].isPerformed)
            {
                activeModulesUpdate[i].Cancel();
            }
        }

        finalVelocity = Vector3.zero;
        playerRb.velocity = Vector3.zero;
    }
}