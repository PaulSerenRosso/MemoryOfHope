using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : ListenerActivate
{
    public Transform SpawnPosition;

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