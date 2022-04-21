using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFirstScreenManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadingGame());
    }

    IEnumerator LoadingGame()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.instance.LoadingScene(1);
    }
    
    // Firstscreen index 0
    // Mainmenu index 1
    // Ingame index 2
    // Pause index 3
    // Options index 4
}
