using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public  class LeaderBoard : MonoBehaviour
{
    // Leaderboard IDs for each level
    private Dictionary<string, string> levelLeaderBoardIDs = new Dictionary<string, string>()
    {
        {"Level1", "21684"},
        {"Level2", "21689"},
        {"Level3", "21688"},
    };

    void Awake()
    {
        // Start the login routine
         StartCoroutine(LoginRoutine());
    }
    IEnumerator LoginRoutine()
    {
        
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Login successful!");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Login failed: " + response.text);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);

        SetPlayerName();
    }
    

    public void SetPlayerName()
    {
        LootLockerSDKManager.SetPlayerName(UsernameManager.GetUsername(), (response) =>
        {
            if (response.success)
            {
                Debug.Log("Player name updated successfully!");
            }
            else
            {
                Debug.Log("Player name update failed: " + response.text);
            }
        });
    }
    
    public void SubmitScore(float time, string level)
    {
        StartCoroutine(SubmitScoreRoutine(time, level));
    }
    
    IEnumerator SubmitScoreRoutine(float time, string level)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, (int)(time*100), levelLeaderBoardIDs[level], (response) =>
        {
            if (response.success)
            {
                Debug.Log("Score submitted successfully!");
                done = true;
            }
            else
            {
                Debug.Log("Score submission failed: " + response.text);
                done = true;
            }
        });

        yield return new WaitWhile(() => done == false);
    }



}
