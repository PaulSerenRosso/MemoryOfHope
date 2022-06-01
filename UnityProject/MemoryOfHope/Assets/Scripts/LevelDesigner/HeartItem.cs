using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HeartItem : MonoBehaviour
{
    private bool isObtained;

    public UnityEvent obtention;
    
    public void GetItem()
    {
        if (isObtained) return;
        obtention?.Invoke();
        isObtained = true;
        PlayerManager.instance.maxHealth += 4;
        int lostHp = PlayerManager.instance.maxHealth - PlayerManager.instance.health;
        UIInstance.instance.GetHeart();
        UIInstance.instance.DisplayHealth();
        PlayerManager.instance.Heal(lostHp);
        var anim = GetComponent<Animation>();
        anim.Play("GetLifeItem");
        StartCoroutine(GetObject());
    }

    IEnumerator GetObject()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
