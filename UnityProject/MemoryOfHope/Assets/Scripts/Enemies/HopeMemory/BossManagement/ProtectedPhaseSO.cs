using UnityEngine;

[CreateAssetMenu(menuName = "BossPhase/ProtectedPhase")]
public class ProtectedPhaseSO : BossPhaseSO
{
    [SerializeField] private GameObject corruptedTowers;
    [SerializeField] private BossPuzzleType difficulty;
    
    public override void SetPhase()
    {
        base.SetPhase();

        BossPhaseManager.instance.bossStateMachine.associatedTowers.Clear();

        foreach (var tr in BossPhaseManager.instance.towersSpawningPoints)
        {
            var tower = Instantiate(corruptedTowers, tr.position, Quaternion.identity, tr).GetComponent<EnemyManager>();
            tower.IsBaseEnemy = false;
            BossPhaseManager.instance.bossStateMachine.associatedTowers.Add(tower);
        }
        
        BossPhaseManager.instance.SetPuzzle(difficulty);

        PlayerManager.instance._shield._laserCharge = 0;
        UIInstance.instance.LaserSlider.value = 0;
    }
}