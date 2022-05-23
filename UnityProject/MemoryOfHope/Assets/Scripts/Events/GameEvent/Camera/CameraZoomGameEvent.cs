using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Event/GameEvent/Camera/CameraZoomGameEvent", order = 1)]
public class CameraZoomGameEvent : ScriptableObject
{
   public float Distance;
   public float DistanceSpeed;
   public float Fov;
   public float FovSpeed;
   public float Offset;
   public float OffsetSpeed;
  public void LaunchZoom()
   {
      if (Distance == 0)
         MainCameraController.Instance.DistanceIsSet = true;
      else
         MainCameraController.Instance.DistanceIsSet = false;
      if (Fov == 0)
         MainCameraController.Instance.FovIsSet = true;
      else
         MainCameraController.Instance.FovIsSet = false;
      if (Offset == 0)
         MainCameraController.Instance.OffsetIsSet = true;
      else
         MainCameraController.Instance.OffsetIsSet = false;
   
      MainCameraController.Instance.CurrentZoom = this;
 
   }
}
