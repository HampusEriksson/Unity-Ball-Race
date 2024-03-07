using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerNew : MonoBehaviour
{
    private Quaternion startRotation;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Change the rotation based on the horizontal mouse movement
        float mouseX = Input.GetAxis("Mouse X");
        transform.RotateAround(player.transform.position, Vector3.up, mouseX * 3);

        // Change the rotation based on the vertical mouse movement
        float mouseY = Input.GetAxis("Mouse Y");
        transform.RotateAround(player.transform.position, Vector3.left, mouseY * 3);

        
        // Make sure the camera is behind the player
        transform.position = player.transform.position - transform.forward * 5;
    }
}
