using UnityEngine;

public class LifeItemRotate : MonoBehaviour
{
    [SerializeField] private float speed;

    void Update()
    {
        transform.eulerAngles += Vector3.up * speed * Time.deltaTime;
    }
}