using UnityEngine;

public class ModuleAcquisition : CheckPoint
{
    [SerializeField] private ParticleSystem notActivatedEffect;
    [SerializeField] private Module[] moduleToLearn;
    public ParticleSystem activateModuleEffect;

    public override void Activate()
    {
        GameManager.instance.RumbleConstant(.1f, .3f, 1);
        
        foreach (var module in moduleToLearn)
        {
            PlayerManager.instance.AddModule(module);
        }
        notActivatedEffect.Stop();
        
        // Pour l'instant : le fx se lance directement (mais à terme dans CinematicManager)
        activateModuleEffect.Play();
        
        base.Activate();
    }
}