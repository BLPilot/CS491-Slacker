using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowFromAbove : MonoBehaviour
{
    //used to get controller componenent
    public GameObject player;

    //controller component of player
    private CharacterController controller;

    //player position
    public Transform playerPos;

    //Editable variables to change position of camera

    //change camPos
    public float camY = 16;
    public float deltaZ;
    public int camSpeed;

    //change cam rotation
    public float camRotation;

    //new position of camera
    private Vector3 newPos;

    //variable for the main cam
    private Camera mainCam;

    //checks if user wants to follow player character
    bool followPlayer = true;

    

    private void Start()
    {
        mainCam = Camera.main;
        mainCam.transform.eulerAngles = new Vector3(camRotation, 0, 0);

        controller = player.GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        if (followPlayer) { 
            Follow();
        }
        else
        {
            MoveFree();
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            followPlayer = !followPlayer;
        }
    }

    public void Follow()
    {
        //enable player controller
        controller.enabled = true;

        //follow the player from a top down view without change to orientation
        newPos = new Vector3(playerPos.position.x, camY, playerPos.position.z - deltaZ);

        //set new position
        mainCam.transform.position = newPos;
    }

    public void MoveFree()
    {
        //disable controller
        controller.enabled = false;

        if (Input.GetKey(KeyCode.D))
        {
            newPos.x += camSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            newPos.z -= camSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            newPos.x -= camSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            newPos.z += camSpeed * Time.deltaTime;
        }

        mainCam.transform.position = newPos;
    }
}
