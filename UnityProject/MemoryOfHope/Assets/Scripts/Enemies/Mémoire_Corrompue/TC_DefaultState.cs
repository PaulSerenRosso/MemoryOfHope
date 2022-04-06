using UnityEngine;

[System.Serializable]
public class TC_DefaultState : EnemyState
{
    private Transform corruptedMemoryTransform;

    public override void StartState(EnemyMachine enemyMachine)
    {
        corruptedMemoryTransform = enemyMachine.GetComponent<CorruptedTowerManager>().linkedCorruptedMemory;
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        Debug.DrawLine(enemyMachine.transform.position, corruptedMemoryTransform.position, Color.yellow);
    }
}
