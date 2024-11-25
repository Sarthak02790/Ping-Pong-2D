using UnityEngine;
using UnityEngine.UI;

public class TapToPlay : MonoBehaviour
{
    public GameObject tapToPlayUI; // Reference to the Tap to Play UI (e.g., a panel or text)

    private bool gameStarted = false; // Tracks whether the game has started

    void Start()
    {
        // Pause the game at the start
        Time.timeScale = 0;
        tapToPlayUI.SetActive(true);
    }

    void Update()
    {
        // Check for a tap or mouse click to start the game
        if (!gameStarted && Input.GetMouseButtonDown(0))
        {
            StartGame();
        }else if(!gameStarted && Input.GetKey(KeyCode.Space))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        gameStarted = true;
        Time.timeScale = 1; // Resume the game
        tapToPlayUI.SetActive(false); // Hide the Tap to Play UI
    }
}
