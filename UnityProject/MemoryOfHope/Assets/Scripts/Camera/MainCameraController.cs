using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public static MainCameraController Instance;
    public Transform viewFinder;
    public float Distance;
    [SerializeField] Vector3 offSet;

    public Camera MainCamera;
    public bool FovIsSet = true;
    public bool DistanceIsSet = true;
    public bool OffsetIsSet = true;
    public CameraZoomGameEvent CurrentZoom;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        Instance = this;
    }

    void LateUpdate()
    {
        if (CurrentZoom != null)
            Zoom();

        transform.position = viewFinder.position + offSet - transform.forward * Distance;
    }


    void Zoom()
    {
        if (FovIsSet && DistanceIsSet && OffsetIsSet)
        {
            CurrentZoom = null;
            return;
        }

        if (!DistanceIsSet)
        {
            if (LerpZoom(Distance, CurrentZoom.Distance, CurrentZoom.DistanceSpeed, out Distance))
            {
                DistanceIsSet = true;
            }
        }

        if (!FovIsSet)
        {
            if (CurrentZoom == null) return;

            var mainFieldOfView = Camera.main.fieldOfView;
            if (LerpZoom(mainFieldOfView, CurrentZoom.Fov, CurrentZoom.FovSpeed, out mainFieldOfView))
            {
                FovIsSet = true;
            }
            else
            {
                Camera.main.fieldOfView = mainFieldOfView;
            }
        }

        if (!OffsetIsSet)
        {
            if (LerpZoom(offSet.y, CurrentZoom.Offset, CurrentZoom.OffsetSpeed, out offSet.y))
            {
                OffsetIsSet = true;
            }
        }
    }

    bool LerpZoom(float _base, float _target, float _speed, out float _value)
    {
        _value = Mathf.Lerp(_base, _target, _speed * Time.deltaTime);
        if (Mathf.Abs(_target - _value) < 0.1f)
            return true;
        return false;
    }
}