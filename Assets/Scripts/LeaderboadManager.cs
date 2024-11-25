using UnityEngine;
using System.Collections.Generic;
using System.IO;

#if UNITY_EDITOR
using UnityEditor; // Required for AssetDatabase usage in the Unity Editor
#endif

public class LeaderboardManager : MonoBehaviour
{
    public List<ScoreEntry> leaderboard; // List to store leaderboard entries
    private string savePath;
    private string editorPath; // Path for saving in Unity Editor's Assets folder

    void Start()
    {
        savePath = Application.persistentDataPath + "/leaderboard.json";

#if UNITY_EDITOR
        editorPath = Application.dataPath + "/leaderboard.json"; // Path for the Assets folder
#endif

        EnsureLeaderboardFileExists(); // Ensure the file exists
        LoadLeaderboard(); // Load the leaderboard
    }

    // Add a new score to the leaderboard
    public void AddScore(string playerName, int score)
    {
        ScoreEntry newEntry = new ScoreEntry { playerName = playerName, score = score };
        leaderboard.Add(newEntry);

        // Sort the leaderboard by score in descending order
        leaderboard.Sort((a, b) => b.score.CompareTo(a.score));

        // Optionally, keep only the top 10 scores
        if (leaderboard.Count > 10)
            leaderboard.RemoveRange(10, leaderboard.Count - 10);

        SaveLeaderboard();
    }

    // Save leaderboard data to a file
    void SaveLeaderboard()
    {
        LeaderboardData data = new LeaderboardData { entries = leaderboard };
        string json = JsonUtility.ToJson(data, true); // Use pretty printing for better readability

        // Save to Persistent Data Path
        File.WriteAllText(savePath, json);
        Debug.Log("Leaderboard saved to persistent path: " + savePath);

#if UNITY_EDITOR
        // Save a copy in the Unity Editor's Assets folder
        File.WriteAllText(editorPath, json);
        Debug.Log("Leaderboard saved to Assets folder: " + editorPath);

        // Refresh AssetDatabase to show the file in the Editor
        AssetDatabase.Refresh();
#endif
    }

    // Load leaderboard data from a file
    void LoadLeaderboard()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            LeaderboardData data = JsonUtility.FromJson<LeaderboardData>(json);
            leaderboard = data.entries;
        }
        else
        {
            leaderboard = new List<ScoreEntry>(); // Initialize empty leaderboard if no file exists
        }
    }

    // Ensure the leaderboard file exists
    void EnsureLeaderboardFileExists()
    {
        if (!File.Exists(savePath))
        {
            leaderboard = new List<ScoreEntry>(); // Initialize an empty leaderboard
            SaveLeaderboard(); // Create the file with default data
        }
    }

    // Reset the leaderboard
    public void ResetLeaderboard()
    {
        leaderboard.Clear(); // Clear all leaderboard entries
        SaveLeaderboard();   // Save the empty leaderboard to the file
    }
}

[System.Serializable]
public class ScoreEntry
{
    public string playerName; // Name of the player
    public int score;         // Player's score
}

[System.Serializable]
public class LeaderboardData
{
    public List<ScoreEntry> entries; // List of leaderboard entries
}
