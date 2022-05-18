using UnityEngine.InputSystem;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance is { })
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        inputs = new InputMaster();
        inputs.Enable();


    }
    
    public InputMaster inputs;
    
    public enum RumblePattern
    {
        Constant,
        Pulse,
        Linear
    }

    private RumblePattern activeRumblePattern;
    private float rumbleDuration;
    private float pulseDuration;
    private float lowA;
    private float highA;
    private float rumbleStep;
    private bool isMotorActive = false;
    private bool isRumbling;

    public void RumbleConstant(float low, float high, float duration)
    {
        if (isRumbling) return;
        Debug.Log("rumble 0");

        isRumbling = true;
        activeRumblePattern = RumblePattern.Constant;
        lowA = low;
        highA = high;
        rumbleDuration = Time.time + duration;
    }

    public void RumblePulse(float low, float high, float burstTime, float duration)
    {
        if (isRumbling) return;
        Debug.Log("rumble 1");

        isRumbling = true;
        activeRumblePattern = RumblePattern.Pulse;

        lowA = low;
        highA = high;

        rumbleStep = burstTime;
        pulseDuration = Time.time + burstTime;
        rumbleDuration = Time.time + duration;

        isMotorActive = true;

        var g = GetGamepad();
        g?.SetMotorSpeeds(lowA, highA);
    }

    public void RumbleLinear(float lowStart, float lowEnd, float highStart, float highEnd, float duration)
    {
        if (isRumbling) return;
        Debug.Log("rumble 2");

        isRumbling = true;
    }

    public void StopRumble()
    {
        var gamepad = GetGamepad();
        if (gamepad == null) return;
        gamepad.SetMotorSpeeds(0, 0);
        isRumbling = false;

    }

    private void Update()
    {
        if (Time.time > rumbleDuration)
        {
            StopRumble();
            return;
        }
        var gamepad = GetGamepad();
        if (gamepad == null) return;

        switch (activeRumblePattern)
        {
            case RumblePattern.Constant:
                gamepad.SetMotorSpeeds(lowA, highA);
                break;
            
            case RumblePattern.Pulse:
                if (Time.time > pulseDuration)
                {
                    isMotorActive = !isMotorActive;
                    pulseDuration = Time.time + rumbleStep;
                    if (!isMotorActive) gamepad.SetMotorSpeeds(0, 0);
                    else gamepad.SetMotorSpeeds(lowA, highA);
                }
                break;
            
            case RumblePattern.Linear:
                break;
            
            default:
                break;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        StopRumble();
    }

    private Gamepad GetGamepad()
    {
        /*
        Gamepad gamepad = null;
        foreach (var g in Gamepad.all)
        {
            foreach (var d in inputs.asset.devices)
            {
                if (d.deviceId == g.deviceId)
                {
                    gamepad = g;
                    break;
                }
            }

            if (gamepad != null)
            {
                break;
            }
        }
        */
        
        return Gamepad.current;
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

}
