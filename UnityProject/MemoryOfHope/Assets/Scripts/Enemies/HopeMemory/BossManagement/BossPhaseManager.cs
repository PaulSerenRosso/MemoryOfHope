using System;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class BossPhaseManager : MonoBehaviour
{
    #region Instance

    public static BossPhaseManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
    }

    #endregion

    public Transform[] spawningPoints;
    public bool isSphereRotating;
    [SerializeField] float rotatingSpeed;
    public Transform rotatingSphere;
    public Transform[] towersSpawningPoints;
    public HM_StateMachine bossStateMachine;
    public List<BossPhaseSO> allPhases;
    public BossPhaseSO currentPhase;
    
    private void Start()
    {
        BeginsBattle(); // A terme : ça se lance pas ici
    }

    private void Update()
    {
        if (isSphereRotating)
        {
            rotatingSphere.eulerAngles += Vector3.up * rotatingSpeed * Time.deltaTime;
        }
    }

    public void BeginsBattle()
    {
        // Le boss est activé
        bossStateMachine.ActivateBehaviour();
    }

    public void SetNextPhase() // Set Next Phase est appelé dans les fonctions du State Machine
    {
        if(currentPhase != null) allPhases.Remove(currentPhase);
        if(allPhases.Count == 0)
        {
            Debug.Log("Boss vaincu");
        }
        else
        {
            isSphereRotating = false;
            currentPhase = allPhases[0];
            switch (currentPhase.phaseType)
            {
                case PhaseType.Vulnerable:
                    var vulnerablePhase = (VulnerablePhaseSO) currentPhase;
                    vulnerablePhase.SetPhase();
                    break;
                case PhaseType.Protected:
                    var protectedPhase = (ProtectedPhaseSO) currentPhase;
                    protectedPhase.SetPhase();
                    break;
                default:
                    Debug.LogError("Type invalide");
                    break;
            }
        }
    }
}
