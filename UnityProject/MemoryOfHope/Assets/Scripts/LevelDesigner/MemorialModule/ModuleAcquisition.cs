using System;
using System.Collections;
using UnityEngine;

public class ModuleAcquisition : CheckPoint
{
    [SerializeField] private ParticleSystem notActivatedEffect;
    [SerializeField] private Module[] moduleToLearn;
    [SerializeField] private ParticleSystem activateModuleEffect;

    public override void Activate()
    {
        GameManager.instance.RumbleConstant(.1f, .3f, 1);
        
        foreach (var module in moduleToLearn)
        {
            PlayerManager.instance.AddModule(module);
        }
        notActivatedEffect.Stop();
        activateModuleEffect.Play();
        base.Activate();
    }
}