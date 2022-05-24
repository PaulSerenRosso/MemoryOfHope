using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

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
    public Transform[] towersInitPos;
    public Transform[] towersSpawningPoints;
    public Transform[] puzzleBoxesSpawningPoints;
    public Transform[] puzzlesBoxes;
    public HM_StateMachine bossStateMachine;
    public BossPhaseSO[] allPhasesSO;
    private List<BossPhaseSO> allPhases = new List<BossPhaseSO>();
    public BossPhaseSO currentPhase;
    public List<EnemyManager> allEnemiesInBossRoom = new List<EnemyManager>();
    [SerializeField] private ListenerTrigger bossActivator;

    public void RotateSphere()
    {
        if (currentPhase == null) return;
        rotatingSphere.eulerAngles += Vector3.up * currentPhase.rotatingSphereSpeed;
    }

    private void FixedUpdate()
    {
        RotateSphere();
    }

    private void Update()
    {
        if (!hasBattleBegun) return;
        if (currentPhase == null) return;

        if (currentPhase.currentWave == null) return;
        if (bossStateMachine.enemyManager.isDead) return;
        if (currentPhase.currentWave.IsWaveCleared())
        {
            currentPhase.SetNextWave();
        }
    }

    public void BeginsBattle()
    {
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
        if (allPhases.Count == 0) return;
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
                for (int i = 0; i < puzzlesBoxes.Length; i++)
                {
                    var box = puzzlesBoxes[i];
                    var posY = box.position.y;
                    var trs = box.GetComponentsInChildren<Transform>();
                    foreach (var tr in trs) tr.localPosition = Vector3.zero;
                    box.position = towersInitPos[i].position;
                    box.position = new Vector3(box.position.x, posY, box.position.z);

                    box.gameObject.SetActive(true);
                }

                break;

            case BossPuzzleType.Hard:
                // On les place de manière aléatoire et on les fait apparaître
                List<Transform> transformRandom = new List<Transform>();
                transformRandom.Clear();
                foreach (var spawnPoint in puzzleBoxesSpawningPoints) transformRandom.Add(spawnPoint);

                foreach (var box in puzzlesBoxes)
                {
                    var posY = box.position.y;
                    var trs = box.GetComponentsInChildren<Transform>();
                    foreach (var tr in trs) tr.localPosition = Vector3.zero;
                    var randomPos = transformRandom[Random.Range(0, transformRandom.Count)];
                    box.position = randomPos.position;
                    box.position = new Vector3(box.position.x, posY, box.position.z);
                    transformRandom.Remove(randomPos);

                    box.gameObject.SetActive(true);
                }

                break;
        }
    }

    public void OnBossDeath()
    {
        Debug.Log("Boss vaincu");
        UIInstance.instance.SetBossDisplay(bossStateMachine.enemyManager, false);

        currentPhase = null;
        
        foreach (var enemy in allEnemiesInBossRoom)
        {
            if (!enemy.gameObject.activeSelf) continue;
            enemy.TakeDamage(enemy.maxHealth);
            enemy.gameObject.SetActive(false);
        }

        PlayerManager.instance.IsActive = false;
    }
}

public enum BossPuzzleType
{
    Easy,
    Hard
}