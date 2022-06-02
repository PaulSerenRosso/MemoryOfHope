using System.Collections;
using UnityEngine;

[System.Serializable]
public class TC_DefaultState : EnemyState
{
    private Transform memoryTransform;
    public Transform protectionWall;
    [SerializeField] private float wallHeight;
    [SerializeField] private float _animationTime;

    [SerializeField] private Collider _wallCollider;
    [SerializeField] private Animation wallAnim;
    public override void StartState(EnemyMachine enemyMachine)
    {
        base.StartState(enemyMachine);
        _wallCollider.gameObject.SetActive(true);
        var enemy = (TC_StateMachine) enemyMachine;

        memoryTransform = enemy.isHopeCorruptedTower
            ? BossPhaseManager.instance.bossStateMachine.transform
            : enemyMachine.GetComponent<CorruptedTowerManager>().linkedCorruptedMemory;
        
        enemy.StartCoroutine(WaitForUpdate());
    }

    IEnumerator WaitForUpdate()
    {

       
        wallAnim.Play("WallFadeInTower");
        _wallCollider.enabled = false;
        
        yield return new WaitForSeconds(_animationTime);
        _wallCollider.enabled = true;
        Debug.Log("bonsorifdsqfdf");
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