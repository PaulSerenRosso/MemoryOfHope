using System.Collections.Generic;
using System.Diagnostics;
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
    [SerializeField] private HM_StateMachine bossStateMachine;
    public List<BossPhaseSO> allPhases;
    public BossPhaseSO currentPhase;
    
    private void Start()
    {
        BeginsBattle(); // A terme : ça se lance pas ici
    }

    public void BeginsBattle()
    {
        // Le boss est activé
        bossStateMachine.ActivateBehaviour();
        if(allPhases.Count == 0) Debug.LogError("Pas de phase disponible");
        currentPhase = allPhases[0];
    }

    public void SetNextPhase() // Set Next Phase est appelé dans les fonctions du State Machine
    {
        allPhases.Remove(currentPhase);
        if(allPhases.Count == 0)
        {
            Debug.Log("Boss vaincu");
        }
        else
        {
            currentPhase = allPhases[0];
        }
    }
}
