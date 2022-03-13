using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Damageable
{


    public int health
    {
        get ;

        set;
    }
    public int maxHealth
    {
        get ;

        set;
    }
    public bool isDead
    {
        get ;

        set;
    }

    public void TakeDamage(int amount);

    public void Heal(int amount);

    public void Death();
}
