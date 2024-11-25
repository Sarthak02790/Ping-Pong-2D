using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public LeaderboardManager leaderboardManager;
    public HighScoreManager highScoreManager;
    public Text leaderboardText;
    public Text playerScoreText;
    public Text playerScore2Text;
    public Text highScoreText;
    public Text targetScoreText; // New text element for displaying the target score
    public Ball ball;

    public GameObject leaderboardPanel; // Reference to the Leaderboard UI panel
   // public GameObject targetScorePanel; // Reference to the Target Score UI panel
    public int matchWinningScore = 10;  // Example match-winning score

    void Start()
    {
        leaderboardPanel.SetActive(false); // Hide the leaderboard at the start
       // targetScorePanel.SetActive(true); // Show the target score panel at the start

        DisplayTargetScore(); // Update the target score text
    }

    void Update()
    {
        // Update scores
        playerScoreText.text = "Player 1: " + highScoreManager.playerScore;
        playerScore2Text.text = "AI Player: " + highScoreManager.aiScore;
        highScoreText.text = "High Score: " + highScoreManager.highScore;
    }

    public void GoalScored(bool isPlayer)
    {
        highScoreManager.AddScore(isPlayer);
        ball.transform.position = Vector2.zero;
        ball.SendMessage("LaunchBall");

        // Check if the match ends (player or AI reaches the winning score)
        if (highScoreManager.playerScore >= matchWinningScore || highScoreManager.aiScore >= matchWinningScore)
        {
            EndMatch();
        }
    }

    void EndMatch()
    {
        // Determine winner and runner-up
        string winner = highScoreManager.playerScore > highScoreManager.aiScore ? "Player 1" : "AI Player";
        string runnerUp = highScoreManager.playerScore > highScoreManager.aiScore ? "AI Player" : "Player 1";

        // Add scores to the leaderboard
        leaderboardManager.AddScore("Player 1", highScoreManager.playerScore);
        leaderboardManager.AddScore("AI Player", highScoreManager.aiScore);

        // Show the leaderboard UI
        EnableLeaderboard(winner, runnerUp);

        // Optionally, pause the game
        Time.timeScale = 0f;
    }

    void EnableLeaderboard(string winner, string runnerUp)
    {
        leaderboardPanel.SetActive(true); // Show the leaderboard panel
        DisplayLeaderboard(winner, runnerUp); // Update the leaderboard text
    }

    void DisplayLeaderboard(string winner, string runnerUp)
    {
        leaderboardText.text = "Leaderboard:\n";

        // Add winner and runner-up
        leaderboardText.text += $"Winner: {winner}\n";
        leaderboardText.text += $"Runner-Up: {runnerUp}\n\n";

        // Display all leaderboard entries
        foreach (var entry in leaderboardManager.leaderboard)
        {
            leaderboardText.text += $"{entry.playerName}: {entry.score}\n";
        }
    }

    void DisplayTargetScore()
    {
        targetScoreText.text = "Winning Score: " + matchWinningScore;
    }

    public void RestartGame()
    {
        // Reset leaderboard and scores
        leaderboardManager.ResetLeaderboard();
        highScoreManager.ResetScores();

        // Reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 0;

        // Reset the ball position
        ball.transform.position = Vector2.zero;

        // Hide leaderboard UI and resume the game
        leaderboardPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
