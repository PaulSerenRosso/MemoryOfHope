using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTransperantScript : MonoBehaviour
{

    [SerializeField] private List<Iam_InTheWay> currentlyInTheWay;
    [SerializeField] private List<Iam_InTheWay> alreadyTransperant;
    [SerializeField] private Transform player;
    private Transform cam;

    private void Awake()
    {
        currentlyInTheWay = new List<Iam_InTheWay>();
        alreadyTransperant = new List<Iam_InTheWay>();

        cam = this.gameObject.transform;
    }

    private void Update()
    {
        GetAllObjectInTheWay();

        MakeObjectSolid();
        MakeObjectTransperant();

    }

    private void GetAllObjectInTheWay()
    {

        currentlyInTheWay.Clear();

        float cameraPlayerDistance = Vector3.Magnitude(cam.position - player.position);

        Ray ray1_Forward = new Ray(cam.position, player.position - cam.position);
        Ray ray1_Backward = new Ray(player.position, cam.position - player.position);

        var hits1_Forward = Physics.RaycastAll(ray1_Forward, cameraPlayerDistance);
        var hits1_Backward = Physics.RaycastAll(ray1_Forward, cameraPlayerDistance);

        foreach (var hit in hits1_Forward)
        {
            if (hit.collider.gameObject.TryGetComponent(out Iam_InTheWay inTheWay))
            {
                if (!currentlyInTheWay.Contains(inTheWay))
                {
                    currentlyInTheWay.Add(inTheWay);
                }
            }
                    
        }
        foreach (var hit in hits1_Backward)
        {
            if (hit.collider.gameObject.TryGetComponent(out Iam_InTheWay inTheWay))
            {
                if (!currentlyInTheWay.Contains(inTheWay))
                {
                    currentlyInTheWay.Add(inTheWay);
                }
            }

        }

    }

    private void MakeObjectTransperant()
    {
        for (int i = 0; i < currentlyInTheWay.Count; i++)
        {
            Iam_InTheWay inTheWay = currentlyInTheWay[i];

            if (!alreadyTransperant.Contains(inTheWay))
            {
                inTheWay.ShowTransperant();
                alreadyTransperant.Add(inTheWay);
            }
        }
    }

    private void MakeObjectSolid()
    {
        for (int i = alreadyTransperant.Count-1; i >= 0; i--)
        {
            Iam_InTheWay wasInTheWay = alreadyTransperant[i];

            if (!currentlyInTheWay.Contains(wasInTheWay))
            {
                alreadyTransperant.Remove(wasInTheWay);
            }
        }
    }

    
        
    

    // Update is called once per frame
   
}
