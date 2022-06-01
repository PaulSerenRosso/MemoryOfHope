using System.Collections;
using UnityEngine;

public class HopeCorruptedMemoryManager : EnemyManager
{
    [SerializeField] private ParticleSystem deathEffect;

    [SerializeField] private Animation _animation;
    public override void Death()
    {
        deathEffect.Play();
        _animation.Play("WallFadeOutTower");

        StartCoroutine(WaitForWallDeathAnimation());
        base.Death();
    }

    IEnumerator WaitForWallDeathAnimation()
    { 
        var towerMachine = (TC_StateMachine) Machine;
        yield return new WaitForSeconds(_timeDeath);
        towerMachine.defaultState.protectionWall.gameObject.SetActive(false);
    }
}
