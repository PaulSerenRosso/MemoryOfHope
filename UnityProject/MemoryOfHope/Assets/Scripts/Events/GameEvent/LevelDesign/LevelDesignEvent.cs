using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Event/GameEvent/Tutorial/LevelDesign", order = 2)]
public class LevelDesignEvent : ScriptableObject
{
    public void OpeningDoor(GameObject door)
    {
        door.SetActive(false);
    }

    public void ActivateLaser()
    {
        
    }

    public void CreateEnemies()
    {
        
    }
}
