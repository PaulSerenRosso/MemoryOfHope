using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;

    private void Awake()
    {
        if (instance is { })
        {
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;
        
        DontDestroyOnLoad(gameObject);
    }

    public void LoadingScene(int index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }

    public void LoadingSceneAsync(int index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(index);
    }

    public void UnloadingSceneAsync(int index)
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(index);
    }
}
