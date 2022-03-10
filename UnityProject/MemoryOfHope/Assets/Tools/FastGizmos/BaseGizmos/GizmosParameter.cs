using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
public static class GizmosParameter 
{
   public static Base CreateParameter(GizmosType type)
    {
        switch (type)
        {
            case GizmosType.Disc :
                return new Disc();
            
        }

        return null; 
    }
   
    
    [Serializable]
    public class Base
    {
        public GizmosType shapeType;
          public Color ColorGizmos;
          public bool DrawWhenSelect;
      
    }

    [Serializable]
    public class Disc : Base
    { 
        public int id;
        public Quaternion rotation;
        public Vector3 position;
        public Vector3 axis;  
        public float size;
        public bool cutoffPlane;
        public float snap;
    }
   
}
