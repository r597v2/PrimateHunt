using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    public CharacterController controller;

    public float normalSpeed = 5f;
    private float speed;
    private float sprintFactor = 2f;
    private float crouchFactor = 0.5f;

    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public CharacterController playerHeight;
    public float normalHeight, crouchHeight;

    private bool isCrouched = false;

    private bool isGrounded;

    public Transform groundCheck;
    public LayerMask groundMask;

    Vector3 velocity;

    void Start()
    {
        speed = normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            // Check sprint
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                speed *= sprintFactor;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed /= sprintFactor;
            }

            // Check crouching
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (!isCrouched)
                {
                    Crouch();
                    speed *= crouchFactor;
                }
                else
                {
                    UnCrouch();
                    speed /= crouchFactor;
                }
            }

            // Move
            controller.Move(move * speed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);


        }
    }

    private void Crouch()
    {
        playerHeight.height = crouchHeight;
        Debug.Log("Crouching");

        isCrouched = true;
    }

    private void UnCrouch()
    {
        playerHeight.height = normalHeight;
        Debug.Log("Un-Crouching");

        isCrouched = false;
    }
}
