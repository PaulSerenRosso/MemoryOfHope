using UnityEngine;

public class SetPlayerToPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.parent = transform;
            Camera.main.transform.parent = transform;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.parent = null;
            Camera.main.transform.parent = null;
        }
    }
}
