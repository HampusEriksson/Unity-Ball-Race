using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
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
        transform.rotation = startRotation;
        transform.position = player.transform.position + new Vector3(0, 5, 5);
    }
}
