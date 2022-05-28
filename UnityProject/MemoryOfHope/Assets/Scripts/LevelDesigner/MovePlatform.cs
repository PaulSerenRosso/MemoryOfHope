using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField]
    private Transform _groundPlatform;

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            MainCameraController.Instance.transform.parent = _groundPlatform;
            PlayerController.instance.SetMoveGround(_groundPlatform);
        
            PlayerController.instance.CancelAllModules();
         
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MainCameraController.Instance.transform.parent = null;
            PlayerController.instance.ResetMoveGround();
         
        }
    }
}