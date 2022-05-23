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

    public bool hasBattleBegun;
    public Transform[] spawningPoints;
    public Transform rotatingSphere;
    public Transform[] towersSpawningPoints;
    public Transform[] puzzleBoxesSpawningPoints;
    public Transform[] puzzlesBoxes;
    public HM_StateMachine bossStateMachine;
    public BossPhaseSO[] allPhasesSO;
    private List<BossPhaseSO> allPhases = new List<BossPhaseSO>();
    public BossPhaseSO currentPhase;
    public List<EnemyManager> allEnemiesInBossRoom = new List<EnemyManager>();
    [SerializeField] private ListenerTrigger bossActivator;
    
    private void Update()
    {
        if (!hasBattleBegun) return;
        if(currentPhase == null) return;
        
        rotatingSphere.eulerAngles += Vector3.up * currentPhase.rotatingSphereSpeed * Time.deltaTime;

        if (currentPhase.currentWave == null) return;
        if (currentPhase.currentWave.IsWaveCleared())
        {
            currentPhase.SetNextWave();
        }
    }

    public void BeginsBattle()
    {
        // Le boss est activé
        allPhases.Clear();
        foreach (var phase in allPhasesSO)
        {
            allPhases.Add(phase);
        }
        bossStateMachine.ActivateBehaviour();
        hasBattleBegun = true;
    }

    public void BattleRefresh()
    {
        if (!hasBattleBegun) return;

        currentPhase = null;
        hasBattleBegun = false;
        bossActivator.ActivateTrigger();
        bossStateMachine.DeactivateBehaviour();
        allEnemiesInBossRoom.Clear();
        DisablePuzzle();
    }

    public void SetNextPhase() // Set Next Phase est appelé dans les fonctions du State Machine
    {
        if (currentPhase != null) allPhases.Remove(currentPhase);
        if (allPhases.Count == 0)
        {
            Debug.Log("Boss vaincu");
            UIInstance.instance.SetBossDisplay(bossStateMachine.enemyManager, false);

            foreach (var enemy in allEnemiesInBossRoom)
            {
                if (!enemy.gameObject.activeSelf) continue;
                enemy.TakeDamage(enemy.maxHealth);
                enemy.gameObject.SetActive(false);
            }
        }
        else
        {
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

    public void DisablePuzzle()
    {
        foreach (var box in puzzlesBoxes)
        {
            box.gameObject.SetActive(false);
        }
    }

    public void SetPuzzle(BossPuzzleType difficulty)
    {
        switch (difficulty)
        {
            case BossPuzzleType.Easy:
                // On les fait directement apparaître
                foreach (var puzzleBox in puzzlesBoxes)
                {
                    puzzleBox.gameObject.SetActive(true);
                }
                break;
            
            case BossPuzzleType.Hard:
                // On les place de manière aléatoire et on les fait apparaître
                List<Transform> transformRandom = new List<Transform>();
                foreach (var spawnPoint in puzzleBoxesSpawningPoints) transformRandom.Add(spawnPoint);

                foreach (var box in puzzlesBoxes)
                {
                    var randomPos = transformRandom[Random.Range(0, transformRandom.Count)];
                    var posY = box.position.y;
                    box.position = randomPos.position;
                    box.position = new Vector3(box.position.x, posY, box.position.z);
                    transformRandom.Remove(randomPos);
                    box.gameObject.SetActive(true);
                }
                break;
        }
    }
}

public enum BossPuzzleType
{
    Easy, Hard
}