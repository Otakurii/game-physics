using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject inGameMenu; // Drag your pause menu UI here
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
        TogglePauseMenu();
    }
    }   
    
    public void TogglePauseMenu()
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

    public void ResumeGame()
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

    public void BackToLevelSelection()
    {
        Time.timeScale = 1f; // Ensure the game isn't paused
        SceneManager.LoadScene("LevelSelection"); // Replace with your level selection scene name
    }

    public void LoadLevel(int levelIndex)
    {
        Time.timeScale = 1f; // Ensure the game isn't paused
        SceneManager.LoadScene(levelIndex);
    }
}
