using UnityEngine;

[CreateAssetMenu(menuName = "BossPhase/ProtectedPhase")]
public class ProtectedPhaseSO : BossPhaseSO
{
    [SerializeField] private GameObject corruptedTowers;
    [SerializeField] private BossPuzzleType difficulty;
    
    public override void SetPhase()
    {
        base.SetPhase();

        foreach (var tr in BossPhaseManager.instance.towersSpawningPoints)
        {
            BossPhaseManager.instance.bossStateMachine.associatedTowers.Clear();
            var tower = Instantiate(corruptedTowers, tr.position, Quaternion.identity, tr).GetComponent<EnemyManager>();
            BossPhaseManager.instance.bossStateMachine.associatedTowers.Add(tower);
        }
        
        BossPhaseManager.instance.SetPuzzle(difficulty);
    }
}