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
            PlayerController.instance.playerRb.velocity = Vector3.zero;
         
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MainCameraController.Instance.transform.parent = null;
            MainCameraController.Instance.IsFixedUpdate = false ;
            PlayerController.instance.ResetMoveGround();
         
        }
    }
}