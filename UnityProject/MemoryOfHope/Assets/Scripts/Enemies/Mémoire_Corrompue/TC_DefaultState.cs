using UnityEngine;

[System.Serializable]
public class TC_DefaultState : EnemyState
{
    private Transform memoryTransform;
    [SerializeField] private Transform module;

    [SerializeField] private Transform protectionWall;
    [SerializeField] private float wallHeight;

    public override void StartState(EnemyMachine enemyMachine)
    { base.StartState(enemyMachine);
        var enemy = (TC_StateMachine) enemyMachine;

        memoryTransform = enemy.isHopeCorruptedTower
            ? BossPhaseManager.instance.bossStateMachine.transform
            : enemyMachine.GetComponent<CorruptedTowerManager>().linkedCorruptedMemory;
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        module.LookAt(memoryTransform);
        module.eulerAngles = new Vector3(-90, module.eulerAngles.y, 0);

        var towerPos = enemyMachine.transform.position;

        var enemyPos = memoryTransform.position;

        // Scale
        float protectionWallSize = Vector3.Distance(towerPos, enemyPos);
        protectionWall.localScale =
            new Vector3(protectionWallSize, wallHeight, protectionWall.localScale.z);
        
        // Position
        var localPos = (enemyPos + towerPos) * .5f;
        protectionWall.position = new Vector3(localPos.x, .5f * (protectionWall.localScale.y - 1), localPos.z);
        
        // Rotation
        float angle;
        var firstSegment = memoryTransform.position.z - enemyMachine.transform.position.z;
        var secondSegment = memoryTransform.position.x - enemyMachine.transform.position.x;
        angle = -Mathf.Atan2(firstSegment, secondSegment) * Mathf.Rad2Deg;

        protectionWall.eulerAngles = new Vector3(0, angle, 0);
    }
}