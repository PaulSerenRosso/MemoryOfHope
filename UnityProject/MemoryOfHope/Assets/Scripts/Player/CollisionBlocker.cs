using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBlocker : MonoBehaviour
{
    [SerializeField] private Collider characterCollider;
    [SerializeField] private Collider blockerCollider;
    
    void Start()
    {
        Physics.IgnoreCollision(characterCollider, blockerCollider);
    }
}
