using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject inGameMenu; // Drag your in-game menu UI here
    public GameObject winningPanel;
    private bool isPaused = false;

    void Start()
    {
        // Ensure the in-game menu is hidden when the game starts
        if (inGameMenu != null)
            inGameMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleInGameMenu();
        }
    }

    public void ToggleInGameMenu()
    {
        if (isPaused)
            ResumeGame();
        else
            OpenInGameMenu();
    }

    public void OpenInGameMenu()
    {
        if (inGameMenu != null)
        {
            inGameMenu.SetActive(true);
            Time.timeScale = 0f; // Pause the game
            isPaused = true;
        }
    }

    public void CloseInGameMenu()
    {
        if (inGameMenu != null)
        {
            inGameMenu.SetActive(false);
            Time.timeScale = 1f; // Resume the game
            isPaused = false;
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // Ensure the game isn't paused
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResumeGame()
    {
        CloseInGameMenu();
    }

    public void SkipLevel()
    {
        Time.timeScale = 1f; // Ensure the game isn't paused
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No more levels to skip to!");
        }
    }

     public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToSelection()
    {
        Time.timeScale = 1f; // Ensure the game isn't paused
        SceneManager.LoadScene("Level Selection"); // Replace with your home scene name
    } 
    
    public void LoadLevel(int levelIndex)
    {
        Time.timeScale = 1f; // Ensure the game isn't paused
        SceneManager.LoadScene(levelIndex);
    }
}
