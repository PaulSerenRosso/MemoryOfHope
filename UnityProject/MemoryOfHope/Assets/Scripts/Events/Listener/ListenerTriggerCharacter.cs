using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenerTriggerCharacter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EventTrigger"))
        {
            other.gameObject.GetComponent<ListenerTrigger>().Raise();
        }

        if (other.CompareTag("EventTriggerStay"))
        {
            other.gameObject.GetComponent<ListenerTriggerStay>().FirstRaise();
        }
    }
    
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("EventTriggerStay"))
        {
            other.gameObject.GetComponent<ListenerTriggerStay>().Raise();
        }
    }
}

