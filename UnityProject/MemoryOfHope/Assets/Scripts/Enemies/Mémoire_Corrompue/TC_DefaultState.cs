using UnityEngine;

[System.Serializable]
public class TC_DefaultState : EnemyState
{
    private Transform memoryTransform;
    public Transform protectionWall;
    [SerializeField] private float wallHeight;

    public override void StartState(EnemyMachine enemyMachine)
    {
        base.StartState(enemyMachine);
        var enemy = (TC_StateMachine) enemyMachine;

        memoryTransform = enemy.isHopeCorruptedTower
            ? BossPhaseManager.instance.bossStateMachine.transform
            : enemyMachine.GetComponent<CorruptedTowerManager>().linkedCorruptedMemory;
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        var towerPos = enemyMachine.transform.position;

        var enemyPos = memoryTransform.position;

        float protectionWallSize = Vector3.Distance(towerPos, enemyPos);
        protectionWall.localScale =
            new Vector3(protectionWallSize, wallHeight, protectionWall.localScale.z);
        
        var localPos = (enemyPos + towerPos) * .5f;
        protectionWall.position = new Vector3(localPos.x, localPos.y, localPos.z);
        protectionWall.localPosition = new Vector3(protectionWall.localPosition.x,
            .5f * (protectionWall.localScale.y - 1), protectionWall.localPosition.z);

        float angle;
        var firstSegment = memoryTransform.position.z - enemyMachine.transform.position.z;
        var secondSegment = memoryTransform.position.x - enemyMachine.transform.position.x;
        angle = -Mathf.Atan2(firstSegment, secondSegment) * Mathf.Rad2Deg;

        protectionWall.eulerAngles = new Vector3(0, angle, 0);
    }
}