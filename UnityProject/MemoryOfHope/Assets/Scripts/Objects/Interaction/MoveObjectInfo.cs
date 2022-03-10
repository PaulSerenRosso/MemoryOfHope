using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectInfo : InteractiveObject
{
    public float speed;

    public TrailPoint[] keyPoints;
    public TrailPoint pointAfter;
    public TrailPoint pointBefore;
    
    public bool goingForward;

    private void Start()
    {
        SetData();
        SetTrailPoint(true, false, 0);
    }

    private void SetData()
    {
        for (int i = 0; i < keyPoints.Length; i++)
        { 
            TrailPoint point = keyPoints[i];
            point.data = this;
            point.thisIndex = i;
            point.motherObject = motherObject.transform;
        }
    }

    public void SetTrailPoint(bool forward, bool enter, int index)
    {
        if (enter) // On est entré
        {
            if (forward) // On va vers l'avant
            {
                if (index == keyPoints.Length - 1) // Fin du trail
                {
                    pointAfter = null;
                    pointBefore = keyPoints[index - 1];
                }
                else
                {
                    pointAfter = keyPoints[index + 1];
                    pointBefore = keyPoints[index - 1];
                }
            }
            else // On va vers l'arrière
            {
                if (index == 0) // Début du trail
                {
                    pointAfter = keyPoints[1];
                    pointBefore = null;
                }
                else
                {
                    pointAfter = keyPoints[index + 1];
                    pointBefore = keyPoints[index - 1];
                }
            }
        }
        else // On est sorti
        {
            if (forward)
            {
                pointAfter = keyPoints[index + 1];
                pointBefore = keyPoints[index];
            }
            else
            {
                pointAfter = keyPoints[index];
                pointBefore = keyPoints[index - 1];
            }
        }
    }
}