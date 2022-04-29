using System;
using System.Collections;
using UnityEngine;

public class ModuleAcquisition : CheckPoint
{
    [SerializeField] private Module[] moduleToLearn;

    public override void Activate()
    {
        foreach (var module in moduleToLearn)
        {
            PlayerManager.instance.AddModule(module);
        }

        base.Activate();
    }
}