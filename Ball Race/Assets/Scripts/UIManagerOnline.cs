using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using LootLocker.Requests;

public class UIManagerOnline : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TextMeshProUGUI usernameDisplay;
    public TextMeshProUGUI highScoreDisplay;
    public TextMeshProUGUI levelDisplay;

    private Dictionary<string, string> levelLeaderBoardIDs = new Dictionary<string, string>()
    {
        {"Level1", "21733"},
        {"Level2", "21734"},
        {"Level3", "21735"},
    };

    public void Start()
    {
        // Load the username from PlayerPrefs
        string username = UsernameManager.GetUsername();
        usernameDisplay.text = "Username: " + username;

        // Reset all displays
        highScoreDisplay.text = "";
        levelDisplay.text = "";     
        UpdateHighScores("Level1");
    }

    public void UpdateHighScores(string level)
    {
        StartCoroutine(FetchHighScores(level));
    }
    
    

    public IEnumerator FetchHighScores(string level)
    {
        Debug.Log("Fetching high scores for " + level + "...");
        bool done = false;
        string leaderBoardID = levelLeaderBoardIDs[level];
        
        LootLockerSDKManager.GetScoreList(leaderBoardID, 10,0, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Scores fetched successfully!");
                string temp = "";

                LootLockerLeaderboardMember[] members = response.items;
                for (int i = 0; i < members.Length; i++)
                {
                    temp += members[i].rank + ". "  + members[i].player.name + " - " + (((float)members[i].score)/100).ToString("F2") + "\n";
                }
                highScoreDisplay.text = temp;
                done = true;
            }
            else
            {
                Debug.Log("Score fetch failed: " + response.text);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    public void SaveUsername()
    {
        Debug.Log("Saving username as " + usernameInput.text + "...");
        string username = usernameInput.text;
        UsernameManager.SetUsername(username);
        usernameDisplay.text = "Username: " + username;
    }
    
}

