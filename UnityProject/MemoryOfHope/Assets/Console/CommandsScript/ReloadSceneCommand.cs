using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "ReloadScene", menuName = "CommandConsole/ReloadScene", order = 1)]
public class ReloadSceneCommand : ConsoleCommand
{

        public int IndexGameScene;

        public override void Execute()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(IndexGameScene);
            Time.timeScale = 1; 
        }
    
}
