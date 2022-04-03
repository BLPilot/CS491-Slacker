using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    public Animator animations;

    public float speed = 5f;
    public float turnSpeed = 180f;

    public Camera mainCamera;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, 0, y);
        move.Normalize();

        controller.Move(move * speed * Time.deltaTime);

        // Face direction of Movement
        if (move != Vector3.zero)
        {
            //transform.forward = move;

            animations.SetBool("Walking", true);

            Quaternion toRotate = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, turnSpeed * Time.deltaTime);
        }
        else { animations.SetBool("Walking", false); }

        mainCamera.transform.position = new Vector3(transform.position.x, mainCamera.transform.position.y, transform.position.z);
    }
}
