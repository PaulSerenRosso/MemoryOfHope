using UnityEngine;

[CreateAssetMenu(menuName = "BossPhase/ProtectedPhase")]
public class ProtectedPhaseSO : BossPhaseSO
{
    [SerializeField] private bool isTowersRotating;
    [SerializeField] private GameObject corruptedTowers;

    public override void SetPhase()
    {
        base.SetPhase();

        foreach (var tr in BossPhaseManager.instance.towersSpawningPoints)
        {
            BossPhaseManager.instance.bossStateMachine.associatedTowers.Clear();
            var tower = Instantiate(corruptedTowers, tr.position, Quaternion.identity, tr).GetComponent<EnemyManager>();
            BossPhaseManager.instance.bossStateMachine.associatedTowers.Add(tower);
        }

        BossPhaseManager.instance.isSphereRotating = isTowersRotating;
    }
}