using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 5f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;

    private bool isGrounded;

    public Transform groundCheck;
    public LayerMask groundMask;
 

    Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed *= 2f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5f;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
