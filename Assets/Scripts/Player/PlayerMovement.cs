using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    public CharacterController controller;
    public Slider staminaBar;
    public float dValue;

    public float stamina;
    float maxStamina;
    public float normalSpeed = 5f;
    private float speed;
    private float sprintFactor = 2f;
    private float crouchFactor = 0.5f;

    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public CharacterController playerHeight;
    public float normalHeight, crouchHeight;

    private bool isCrouched = false;
    private bool staminaDecreasing;
    public bool isSprinting;

    private bool isGrounded;

    public Transform groundCheck;
    public LayerMask groundMask;

    Vector3 velocity;

    void Start()
    {
        speed = normalSpeed;
        maxStamina = stamina;
        staminaBar.maxValue = maxStamina;
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

            if (stamina <= 0 || stamina <= 0 && isSprinting)
            {
                DisableSprint();
            }

            // Check sprint
            if (Input.GetKeyDown(KeyCode.LeftShift) && stamina > 0)
            {
                Sprint();
            }

            if (stamina > 0)
            {
                sprintFactor = 2f;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                StopSprinting();
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

        UpdateStamina(staminaDecreasing);
    }

    private void Sprint()
    {
        speed *= sprintFactor;
        staminaDecreasing = true;
        isSprinting = true;
    }

    private void StopSprinting()
    {
        speed /= sprintFactor;
        staminaDecreasing = false;
        isSprinting = false;
    }

    private void DisableSprint()
    {
        StopSprinting();
        sprintFactor = 1f;
    }

    private void UpdateStamina(bool decreasing)
    {
        if (decreasing && stamina > 0)
        {
            stamina -= dValue * Time.deltaTime;
            staminaBar.value = stamina;
        }
        else if (Input.GetKeyDown(KeyCode.B) && !decreasing && stamina < maxStamina)
        {
            stamina += dValue;
            staminaBar.value = stamina;
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
