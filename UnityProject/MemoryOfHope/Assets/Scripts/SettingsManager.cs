using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;
    
    public void Awake()
    {
        if (instance is { })
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        
        DontDestroyOnLoad(gameObject);
    }

    public Language gameLanguage;


    public void SetLanguage(Language language)
    {
        gameLanguage = language;
        
        // Set les textes présents
    }
    
}
public enum Language
{
    French,
    English,
}