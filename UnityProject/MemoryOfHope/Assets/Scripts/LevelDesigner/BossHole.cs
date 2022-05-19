using UnityEngine;

public class BossHole : MonoBehaviour
{
    [SerializeField] private Transform respawnPlatform;
    [SerializeField] private int holeDamage = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager.instance.TakeDamage(holeDamage);
            PlayerManager.instance.transform.position = respawnPlatform.position;
        }
    }
}
