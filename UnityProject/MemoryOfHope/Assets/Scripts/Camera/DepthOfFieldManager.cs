using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DepthOfFieldManager : MonoBehaviour
{
    Ray raycast;
    RaycastHit hit;
    bool isHit;
    float hitDistance;
    private object depthOfField;

    // Update is called once per frame
    public void Update()
    {
        raycast = new Ray(transform.position, transform.forward * 100);

        isHit = false;

        if (Physics.Raycast(raycast, out hit, 100f))
        {
            isHit = true;
            hitDistance = Vector3.Distance(transform.position, hit.point);
        }
        else
        {
            if (hitDistance < 100f)
            {
                hitDistance++;
            }
        }


    }

    
    private void OnDrawGizmos()
    {
        if (isHit)
        {
            Gizmos.DrawSphere(hit.point, 0.1f);

            Debug.DrawRay(transform.position, transform.forward * Vector3.Distance(transform.position, hit.point));

        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 100f);
        }
    }
}
