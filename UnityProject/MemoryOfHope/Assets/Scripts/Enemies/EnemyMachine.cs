using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMachine : MonoBehaviour
{
    public EnemyState currentState;
    public Material material;

    public NavMeshAgent agent;


    public virtual void OnHit() // S'active quand l'ennemi a été touché
    {
        Debug.Log("Hit !");
    }
    
    public void OnDisable()
    {
        material.color = Color.white;
    }
}
