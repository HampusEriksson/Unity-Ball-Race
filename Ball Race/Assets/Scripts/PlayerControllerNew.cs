using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerNew : MonoBehaviour
{
    private float speed = 20.0f;
    private Rigidbody playerRb;

    private float maxVelocity = 30.0f;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 startPos;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        startPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameManager.levelComplete){
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        if(verticalInput != 0 || horizontalInput != 0)
        {
         gameManager.StartLevel();   
        }

        // Reset when R is pressed
        if(Input.GetKeyDown(KeyCode.R))
        {
            transform.position = startPos;
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
            gameManager.ResetLevel();
        }
        




        // Apply a force that moves the player forward based on the main camera's orientation
        playerRb.AddForce(Camera.main.transform.forward * speed * verticalInput, ForceMode.Force);


        // Apply a force that moves the player right based on the main camera's orientation
        playerRb.AddForce(Camera.main.transform.right * speed * horizontalInput, ForceMode.Force);

        // Make sure the player doesn't exceed a certain velocity
        if (playerRb.velocity.magnitude > maxVelocity)
        {
            playerRb.velocity = playerRb.velocity.normalized * maxVelocity;
        }

        }
        
    }

    
}
