using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class BossPhase
{
    private BossPhaseSO bossPhase;
    public List<Transform> spawningPoints;
    public UnityEvent activateEvent;
    
}
