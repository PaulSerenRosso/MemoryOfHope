using UnityEngine;

public class CorruptedTowerManager : EnemyManager
{
    public Transform linkedCorruptedMemory;
    [SerializeField] private ParticleSystem deathEffect;

    public override void Death()
    {
        deathEffect.Play();
        var towerMachine = (TC_StateMachine) Machine;
        towerMachine.defaultState.protectionWall.gameObject.SetActive(false);
        base.Death();
        var machine = linkedCorruptedMemory.GetComponent<MC_StateMachine>();
        machine.isProtected = machine.IsProtected();
    }
}
