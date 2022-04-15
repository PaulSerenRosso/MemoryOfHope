using UnityEngine;

[System.Serializable]
public class TC_DefaultState : EnemyState
{
    private Transform corruptedMemoryTransform;
    [SerializeField] private LineRenderer link;

    public override void StartState(EnemyMachine enemyMachine)
    {
        corruptedMemoryTransform = enemyMachine.GetComponent<CorruptedTowerManager>().linkedCorruptedMemory;
        link.positionCount = 2;
        link.SetPosition(0, enemyMachine.transform.position);
        link.SetPosition(1, Vector3.zero);

    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        Debug.DrawLine(enemyMachine.transform.position, corruptedMemoryTransform.position, Color.yellow);
    }
}
