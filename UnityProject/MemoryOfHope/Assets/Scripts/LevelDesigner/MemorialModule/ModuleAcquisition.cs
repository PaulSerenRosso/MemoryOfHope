using System;
using System.Collections;
using UnityEngine;

public class ModuleAcquisition : CheckPoint
{
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private Module[] moduleToLearn;

    public override void Activate()
    {
        GameManager.instance.RumbleConstant(.1f, .3f, 1);
        
        foreach (var module in moduleToLearn)
        {
            PlayerManager.instance.AddModule(module);
        }
        particleSystem.Stop();

        base.Activate();
    }
}