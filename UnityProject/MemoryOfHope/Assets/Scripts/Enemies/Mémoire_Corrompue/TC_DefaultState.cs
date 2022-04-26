using UnityEngine;

[System.Serializable]
public class TC_DefaultState : EnemyState
{
    private Transform corruptedMemoryTransform;
    [SerializeField] private LineRenderer link;
    [SerializeField] private LayerMask player;
    [SerializeField] private int linkDamage;
    [SerializeField] private float damagingDelay;
    [SerializeField] private Transform lookAtTransform;
    
    private float time;
    
    public override void StartState(EnemyMachine enemyMachine)
    {
        corruptedMemoryTransform = enemyMachine.GetComponent<CorruptedTowerManager>().linkedCorruptedMemory;
        link.positionCount = 2;
    }

    public override void UpdateState(EnemyMachine enemyMachine)
    {
        var towerPos = enemyMachine.transform.position;
        var pos0 = new Vector3(towerPos.x, 1, towerPos.z);
        
        var enemyPos = corruptedMemoryTransform.position;
        var pos1 = new Vector3(enemyPos.x, 1, enemyPos.z);
        
        link.SetPosition(0, pos0);
        link.SetPosition(1, pos1);

        var distance = Vector3.Distance(pos0, pos1);
        lookAtTransform.LookAt(pos1);
        if (!Physics.Raycast(pos0, lookAtTransform.forward, distance, player))
        {
            time = 0;
            return;
        }
        if (time >= damagingDelay)
        {
            PlayerManager.instance.TakeDamage(linkDamage);
            time = 0;
        }
        else
        {
            time += Time.deltaTime;
        }
    }
}
