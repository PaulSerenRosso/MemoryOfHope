using UnityEngine;

public class HeartItem : MonoBehaviour
{
    public void GetItem()
    {
        gameObject.SetActive(false);
        PlayerManager.instance.maxHealth += 4;
        int lostHp = PlayerManager.instance.maxHealth - PlayerManager.instance.health;
        UIInstance.instance.GetHeart();
        UIInstance.instance.DisplayHealth();
        PlayerManager.instance.Heal(lostHp);
    }
}
