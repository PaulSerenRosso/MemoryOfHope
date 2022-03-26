using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewFinderCameraController : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    Vector3 offSet;
[SerializeField]
    private bool focusPlayer;
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if(focusPlayer)
        transform.position = player.position + offSet;
        
    }
}
