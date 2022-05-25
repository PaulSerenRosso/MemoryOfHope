using UnityEngine;

public class EntityLookAtPlayer : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
        player = PlayerController.instance.transform;
    }

    void Update()
    {
        transform.LookAt(player);
        var eulerAnglesPos = transform.eulerAngles;
        transform.eulerAngles = new Vector3(0, eulerAnglesPos.y, 0);
    }
}
