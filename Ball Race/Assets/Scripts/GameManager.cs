using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    private float timer = 0.0f;
    private bool levelStarted = false;
    public bool levelComplete = false;
    public TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(levelStarted && !levelComplete)
        {
            timer += Time.deltaTime;
            timerText.text = "Time: " + timer.ToString("F2");
        }
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
        // Load the main menu scene after 5 seconds
        Invoke("LoadMainMenu", 2);
    }

    void LoadMainMenu()
    {
        // Load the main menu scene
        FindObjectOfType<LevelManager>().LoadScene("MainMenu");
    }

    void SetHighScore()
    {
        // Get the username
        string username = UsernameManager.GetUsername();
        
        // Get the current scene name
        string sceneName = SceneManager.GetActiveScene().name;

        // Create the text to append to the file
        string textToAppend = timer.ToString("F2") + ";" + username + "\n"; // Assuming timer is a variable you have declared elsewhere
        
        // Append the text to the file
        File.AppendAllText("Assets/Resources/" + sceneName + ".txt", textToAppend);
    }

    public void ResetLevel()
    {
        levelStarted = false;
        levelComplete = false;
        timer = 0.0f;
        timerText.text = "Time: 0.00";
    }
}
