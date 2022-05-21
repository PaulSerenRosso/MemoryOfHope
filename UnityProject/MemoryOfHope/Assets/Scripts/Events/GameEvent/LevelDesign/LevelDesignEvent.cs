using System.Collections;
using UnityEngine;
using Unity.Mathematics;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Event/GameEvent/Tutorial/LevelDesign", order = 2)]
public class LevelDesignEvent : ScriptableObject
{
    public void OpeningDoor(GameObject door)
    {
        door.SetActive(false);
    }

    public void Teleporting(Transform teleportingArea)
    {
        PlayerController.instance.transform.position = teleportingArea.position;
    }
}
