using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : ListenerActivate
{
    public Transform SpawnPosition;

    public override void Raise()
    {
        if (isActivate)
            PlayerManager.instance.currentCheckPoint = this;

        base.Raise();
        Debug.Log("Checkpoint");
    }


    public override void Activate()
    {
        isActivate = true;
        PlayerManager.instance.currentCheckPoint = this;
        base.Activate();
    }
}