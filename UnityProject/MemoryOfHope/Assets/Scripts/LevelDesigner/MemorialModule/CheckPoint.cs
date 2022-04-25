using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : ListenerActivate
{
    public Transform SpawnPosition;

    public override void Raise()
    {
        if (isActivate)
            PlayerManager.instance.CurrentCheckpoint = this;

        base.Raise();
        Debug.Log("Checkpoint");
    }


    public override void Activate()
    {
        isActivate = true;
        PlayerManager.instance.CurrentCheckpoint = this;
        base.Activate();
    }
}