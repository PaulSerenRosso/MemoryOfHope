using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public float moveSpeed;
    public Vector3 direction;
    
    void Start()
    {
    }

    void Update()
    {
        MovingCuve();
    }

    void MovingCuve()
    {
        transform.position = transform.position + direction * moveSpeed;

    }
}
