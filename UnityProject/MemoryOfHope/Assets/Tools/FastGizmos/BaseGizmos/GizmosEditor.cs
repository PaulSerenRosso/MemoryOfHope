using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GizmosEditor : Editor
{
    private static bool CanDraw(GizmosParameter.Base baseParameter, GameObject obj)
    {
        if (baseParameter.DrawWhenSelect && Selection.activeObject != obj)
            return false;
            return true;
    }
    private static void Base(GizmosParameter.Base baseParameter)
    {
        Handles.color = baseParameter.ColorGizmos;
    }
    public static void Disc(GizmosParameter.Disc discParameter,GameObject obj )
    {
        if (!CanDraw(discParameter, obj))
            return;
        
        Base(discParameter);
            Handles.Disc(discParameter.id, discParameter.rotation, discParameter.position, discParameter.axis,
                discParameter.size, discParameter.cutoffPlane, discParameter.snap); 
        
        
    }
}
