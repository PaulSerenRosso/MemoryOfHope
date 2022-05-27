using UnityEngine;

public class HopeCorruptedMemoryManager : EnemyManager
{
    [SerializeField] private ParticleSystem deathEffect;

    public override void Death()
    {
        deathEffect.Play();
        var towerMachine = (TC_StateMachine) Machine;
        towerMachine.defaultState.protectionWall.gameObject.SetActive(false);
        base.Death();
    }
}
