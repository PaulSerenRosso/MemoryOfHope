using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Event/GameEvent/Tutorial/LevelDesign", order = 2)]
public class LevelDesignEvent : ScriptableObject
{
    public void OpeningDoor(GameObject door)
    {
        door.GetComponent<Animator>().SetBool("isOpened", true);
    }

    public void Teleporting(Transform teleportingArea)
    {
        Feedbacks.instance.TeleportationFeedback(teleportingArea.position);
    }
}
