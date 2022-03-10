using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMachine : MonoBehaviour
{
    public EnemyState currentState;
    public Material material;

    public NavMeshAgent agent;


    public void OnDisable()
    {
        material.color = Color.white;
    }
}
