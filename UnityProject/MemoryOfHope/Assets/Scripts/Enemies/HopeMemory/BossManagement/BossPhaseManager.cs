using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

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

    [SerializeField] private PhaseType currentPhaseType;
    private InvulnerablePhase currentInvulnerablePhase;
    private ProtectedPhase currentProtectedPhase;

    public List<InvulnerablePhase> invulnerablePhases;
    public List<ProtectedPhase> protectedPhases;
    
    public List<Transform> spawningPoints;
    [SerializeField] private HM_StateMachine bossStateMachine;

    private void Start()
    {
        BeginsBattle(); // A terme : ça se lance pas ici
    }

    public void BeginsBattle()
    {
        currentInvulnerablePhase = invulnerablePhases[0];
        currentPhaseType = PhaseType.Invulnerable;
        SetCurrentPhase(currentPhaseType);
            
        // Le boss est activé
        bossStateMachine.ActivateBehaviour();
    }

    public void SetNextInvulnerablePhase(PhaseType type)
    {
        switch (type)
        {
            case PhaseType.Invulnerable:
                
                if (invulnerablePhases.Count <= 1) return;
                if (currentInvulnerablePhase == null) return;
                if (!invulnerablePhases.Contains(currentInvulnerablePhase)) return;

                currentProtectedPhase = null;

                invulnerablePhases.Remove(invulnerablePhases[0]);
                currentInvulnerablePhase = invulnerablePhases[0];
                
                break;
            
            case PhaseType.Protected:
                
                if (protectedPhases.Count <= 1) return;
                if (currentProtectedPhase == null) return;
                if (!protectedPhases.Contains(currentProtectedPhase)) return;

                currentInvulnerablePhase = null;

                protectedPhases.Remove(protectedPhases[0]);
                currentProtectedPhase = protectedPhases[0];
                break;
        }
        
        SetCurrentPhase(currentPhaseType);
    }

    public void SetCurrentPhase(PhaseType type)
    {
        switch (type)
        {
            case PhaseType.Invulnerable:
                
                
                break;
            
            case PhaseType.Protected:
                
                
                break;
        }
    }
}
