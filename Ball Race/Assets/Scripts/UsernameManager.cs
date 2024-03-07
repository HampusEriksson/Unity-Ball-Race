using UnityEngine;

public class UsernameManager : MonoBehaviour
{
    // Key to store the username in PlayerPrefs
    private const string usernameKey = "PlayerUsername";

    // Static method to set the username
    public static void SetUsername(string username)
    {
        PlayerPrefs.SetString(usernameKey, username);
    }

    // Static method to get the username
    public static string GetUsername()
    {
        return PlayerPrefs.GetString(usernameKey);
    }
}
