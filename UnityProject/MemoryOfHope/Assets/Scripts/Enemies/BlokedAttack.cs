using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockedAttack : MonoBehaviour
{
    [SerializeField] private EnemyManager enemy;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield"))
        {
            enemy.isBlocked = true;
        }
    }
    //reset quand l'attaque est terminé pour le prototype ou quand le songe te hurte et knockack
    // qaund ca passe true 
}
