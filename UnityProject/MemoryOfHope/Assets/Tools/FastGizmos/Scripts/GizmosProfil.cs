using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class GizmosProfil : ScriptableObject
{
public GizmosProfilEnum ProfilName;
 public List<FastGizmos> AllGizmos = new List<FastGizmos>();

 public void Reset()
 {
  ProfilName = GizmosProfilEnum.None; 
  AllGizmos.Clear();
  AllGizmos.Add(new FastGizmos());
 }
}

