using UnityEngine;

public class CorruptedTowerManager : EnemyManager
{
    public Transform linkedCorruptedMemory;

    public override void Death()
    {
        base.Death();
        var machine = linkedCorruptedMemory.GetComponent<MC_StateMachine>();
        machine.isProtected = machine.IsProtected();
    }
}
