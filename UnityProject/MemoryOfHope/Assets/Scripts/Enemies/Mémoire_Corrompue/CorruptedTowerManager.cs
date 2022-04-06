using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CorruptedTowerManager : EnemyManager
{
    public Transform linkedCorruptedMemory;

    public override void Death()
    {
        base.Death();
        MC_StateMachine machine = linkedCorruptedMemory.GetComponent<MC_StateMachine>();
        machine.isProtected = machine.IsProtected();
    }
}
