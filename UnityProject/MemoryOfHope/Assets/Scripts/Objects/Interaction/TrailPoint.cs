using System;
using UnityEngine;

public class TrailPoint : MonoBehaviour
{
    public Transform motherObject;
    public MoveObjectInfo data;
    public int thisIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform == motherObject)
        {
            motherObject.position = transform.position; // On place l'objet interactif sur le trail
            
            data.SetTrailPoint(data.goingForward, true, thisIndex);
            // On set les valeurs de Transform
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform == motherObject)
        {
            data.SetTrailPoint(data.goingForward, false, thisIndex);
        }
    }
}
