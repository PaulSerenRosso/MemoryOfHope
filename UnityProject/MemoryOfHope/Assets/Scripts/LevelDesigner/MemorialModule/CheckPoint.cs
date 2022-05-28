using UnityEngine;

public class CheckPoint : ListenerActivate
{
    public Transform SpawnPosition;
    public Vector3 cameraRotation;

    public override void Raise()
    {
        base.Raise();
        Debug.Log("Checkpoint");
    }


    public override void Activate()
    {
        IsActiveTrigger = false; 
        PlayerManager.instance.CheckPointsReached.Add(this);
       
        base.Activate();
    }
}