using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{   
    public static ChangeScenes instance;

    // List of scene names or indexes in the order of levels
    [SerializeField] private string[] levelScenes;

    // Tracks the current level index
    private int currentLevelIndex = 0;

    private void Awake()
    {
        // Singleton pattern to ensure only one LevelManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist through scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadNextLevel()
    {
        // Check if there is a next level
        if (currentLevelIndex < levelScenes.Length - 1)
        {
            
            SceneManager.LoadScene(levelScenes[currentLevelIndex]);
            currentLevelIndex++;
        }
        else
        {
            Debug.Log("No more levels. Game complete!");
            // Optionally, load an end screen or restart from the first level
            // SceneManager.LoadScene("EndScreen");
        }
    }

    public void RestartLevel()
    {
        // Reloads the current level
        SceneManager.LoadScene(levelScenes[currentLevelIndex]);
    }

    public void LoadLevel(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < levelScenes.Length)
        {
            currentLevelIndex = levelIndex;
            SceneManager.LoadScene(levelScenes[currentLevelIndex]);
        }
        else
        {
            Debug.LogError("Level index out of bounds");
        }
    }
}
