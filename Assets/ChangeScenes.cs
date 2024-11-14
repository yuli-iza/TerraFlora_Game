using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{   
    public string sceneName = "Scene_0";

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(sceneName);
            
    }

}
