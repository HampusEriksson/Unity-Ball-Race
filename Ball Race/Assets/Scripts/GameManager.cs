using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float timer = 0.0f;
    private bool levelStarted = false;
    public bool levelComplete = false;
    public TextMeshProUGUI timerText;
    private LeaderBoard leaderBoard;

    void Start()
    {
        leaderBoard = FindObjectOfType<LeaderBoard>();
    }

    void Update()
    {
        // If the level has started and the level is not complete update the timer
        if(levelStarted && !levelComplete)
        {
            timer += Time.deltaTime;
            timerText.text = "Time: " + timer.ToString("F2");
        }

        // If the level is complete display the time taken to complete the level
        else if(levelComplete)
        {
            timerText.text = "Level Complete!\nTime: " + timer.ToString("F2") + " seconds!";
        }
        
    }

    public void StartLevel()
    {
        levelStarted = true;
    }

    public void CompleteLevel()
    {
        levelComplete = true;
        SetHighScore();
        // Load the main menu scene after 1 second
        Invoke("LoadMainMenu", 1);
    }

    void LoadMainMenu()
    {
        // Load the main menu scene
        FindObjectOfType<LevelManager>().LoadScene("MainMenuOnline");
    }

    void SetHighScore()
    {        
        // Get the current scene name
        string sceneName = SceneManager.GetActiveScene().name;

        leaderBoard.SubmitScore(timer, sceneName);
    }

    public void ResetLevel()
    {
        levelStarted = false;
        levelComplete = false;
        timer = 0.0f;
        timerText.text = "Time: 0.00";
    }
}
