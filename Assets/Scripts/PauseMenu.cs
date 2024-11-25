using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu; // Reference to the Pause Menu panel
    public GameObject pauseButton; // Reference to the Pause Button
    public GameObject tapToPlay; // Reference to the "Tap to Play" GameObject
    private bool isPaused = false; // Tracks whether the game is paused

    private void Start()
    {
        // Initially hide the Pause Button when "Tap to Play" is active
        if (tapToPlay.activeSelf)
        {
            pauseButton.SetActive(false);
        }
    }

    private void Update()
    {
        // Prevent game start when paused
        if (isPaused)
            return;

        // Check if the game has started by checking "Tap to Play" deactivation
        if (!tapToPlay.activeSelf && !pauseButton.activeSelf)
        {
            pauseButton.SetActive(true);
        }

        // Example tap-to-play logic (if your game uses a screen tap to start)
        if (tapToPlay.activeSelf && Input.GetMouseButtonDown(0))
        {
            StartGame();
        }
    }

    public void PauseMenuButton()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true; // Mark the game as paused
        Debug.Log("PauseMenu");
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false; // Mark the game as resumed
        Debug.Log("Resume");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        isPaused = false; // Ensure game is not paused after restart
        Debug.Log("Restart");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    private void StartGame()
    {
        // Logic for starting the game
        tapToPlay.SetActive(false);
        Debug.Log("Game Started");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
