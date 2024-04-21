using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    
    void Update()
    {
        // Make sure the camera follows the player
        transform.position = player.transform.position + new Vector3(0, 5, 5);
    }
}
