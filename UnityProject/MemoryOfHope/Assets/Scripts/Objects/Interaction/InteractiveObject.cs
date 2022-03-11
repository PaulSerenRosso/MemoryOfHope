using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    #region Player Position

    public Transform playerTransform;

    public bool isActive;
    public GameObject motherObject;

    #endregion

    public InteractionType interactionType;
    public Type interactiveModuleType;
    
}

public enum InteractionType
{
    Move, Rotate
}
