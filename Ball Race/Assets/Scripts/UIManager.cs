using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine.Networking;

public class UIManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TextMeshProUGUI usernameDisplay;
    public TextMeshProUGUI highScoreDisplay;
    public TextMeshProUGUI levelDisplay;


    public void Start()
    {
        // Load the username from PlayerPrefs
        string username = UsernameManager.GetUsername();
        usernameDisplay.text = "Username: " + username;
        // Reset all displays
        highScoreDisplay.text = "";
        levelDisplay.text = "";
        
    }
    public void SaveUsername()
    {
        Debug.Log("Saving username as " + usernameInput.text + "...");
        string username = usernameInput.text;
        UsernameManager.SetUsername(username);
        usernameDisplay.text = "Username: " + username;
    }

    public void DisplayHighScore(string level)
    {
        Debug.Log("Displaying high score for level " + level + "...");
        // Load from the file "Assets/Resources/" + level + ".txt" and display the high score
        // Read the contents of the file
        string filePath = "Assets/Resources/" + level + ".txt";
        string[] lines = File.ReadAllLines(filePath);

        // Define a class to hold the score and username
        List<ScoreEntry> scores = new List<ScoreEntry>();

        // Parse each line and add it to the scores list
        foreach (string line in lines)
        {
            string[] parts = line.Split(';');
            if (parts.Length == 2)
            {
                if (float.TryParse(parts[0], out float time))
                {
                    scores.Add(new ScoreEntry(time, parts[1]));
                }
            }
        }

        // Sort the scores by time (ascending)
        scores.Sort((a, b) => a.time.CompareTo(b.time));
        levelDisplay.text = level;
        highScoreDisplay.text = "";
        // Display the top 5 scores
        int count = Mathf.Min(scores.Count, 5);
        for (int i = 0; i < count; i++)
        {
            highScoreDisplay.text += (i + 1) + ". " + scores[i].username + ": " + scores[i].time.ToString("F2") + "\n";
        }
    }

    
    
}


public class ScoreEntry
    {
        public float time;
        public string username;

        public ScoreEntry(float time, string username)
        {
            this.time = time;
            this.username = username;
        }
    }