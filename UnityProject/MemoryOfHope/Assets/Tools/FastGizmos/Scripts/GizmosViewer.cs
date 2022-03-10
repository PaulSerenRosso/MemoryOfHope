using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosViewer : MonoBehaviour
{
    public FastGizmos[] FastGizmosList;
    
    void OnValidate()
    {
        for (int i = 0; i < FastGizmosList.Length; i++)
        {
            if (FastGizmosList[i].Parameter == null)
            {
              FastGizmosList[i].Parameter = GizmosParameter.CreateParameter(FastGizmosList[i].Type);
            }
        }
    }
}
