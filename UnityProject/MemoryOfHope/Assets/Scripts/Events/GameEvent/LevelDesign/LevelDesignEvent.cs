using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Event/GameEvent/Tutorial/LevelDesign", order = 2)]
public class LevelDesignEvent : ScriptableObject
{
    public void OpeningDoor(GameObject door)
    {
        door.GetComponent<Animator>().Play("OpenDoor");
        Debug.Log("bien joué");
    }

    public void ClosingDoor(GameObject door)
    {
        door.GetComponent<Animator>().Play("CloseDoor");
        Debug.Log("tah la fermeture");
    }

    public void Teleporting(Transform teleportingArea)
    {
        Feedbacks.instance.TeleportationFeedback(teleportingArea.position);
    }
}
