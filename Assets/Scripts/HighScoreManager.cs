using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HighScoreManager : MonoBehaviour
{
    public int playerScore;
    public int aiScore;
    public int highScore;

    private string savePath;

    void Start()
    {
        savePath = Application.persistentDataPath + "/highscore.json";
        LoadHighScore(); // Ensure the high score is loaded on start
    }

    public void AddScore(bool isPlayer)
    {
        // Increment the respective score based on who scored
        if (isPlayer)
            playerScore++;
        else
            aiScore++;

        // Update high score if either player or AI surpasses it
        if (playerScore > highScore || aiScore > highScore)
        {
            highScore = Mathf.Max(playerScore, aiScore);  // Ensure the highest score is saved
            SaveHighScore(); // Save the new high score
        }
    }

    void SaveHighScore()
    {
        HighScoreData data = new HighScoreData { highScore = highScore };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json); // Save to file
    }

    void LoadHighScore()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            HighScoreData data = JsonUtility.FromJson<HighScoreData>(json);
            highScore = data.highScore; // Load high score from file
        }
        else
        {
            highScore = 0; // Default high score if none exists
        }
    }

    // Reset the scores when starting a new game
    public void ResetScores()
    {
        playerScore = 0;
        aiScore = 0;
    }
}

[System.Serializable]
public class HighScoreData
{
    public int highScore;
}
