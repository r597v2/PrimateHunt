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
    public float obesity;

    public float stamina;
    float maxStamina;
    public float normalSpeed = 5f;
    private float speed;
    private float sprintFactor = 2f;
    private bool isSprinting;
    private float crouchFactor = 0.5f;

    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public CharacterController playerHeight;
    public float normalHeight, crouchHeight;

    private bool isCrouched = false;
    private bool staminaDecreasing;

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

            //check if the stamina is 0, then disable sprinting
            if (stamina <= 0)
            {
                DisableSprint();
            }

            // Check sprint
            if (Input.GetKeyDown(KeyCode.LeftShift) && stamina > 0)
            {
                Sprint();
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

        //eating bananas 
        if (Input.GetKeyDown(KeyCode.B))
        {
            InventoryItemData itemData = new InventoryItemData(InventoryItemData.ITEMTYPES.BANANA);
            InventoryHolder ih = GetComponent<InventoryHolder>();

            if (ih.InventorySystem.RetrieveFromInventory(itemData, 1))
            {
                if (stamina >= maxStamina)
                {
                    stamina -= dValue * obesity; //remove dValue * obesity from stamina
                    transform.Find("VomitControl").GetComponent<VomitControl>().Vomit(); //vomit
                    staminaBar.value = stamina; //update the stamina
                    DisableSprint();
                }
                else if (!staminaDecreasing && stamina < maxStamina)
                {
                    EnableSprint();
                    sprintFactor = 2f;
                    stamina += dValue;
                    staminaBar.value = stamina;
                }
            }
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
        if (isSprinting)
        {
            StopSprinting();
        }

        sprintFactor = 1f;
        dValue = 0;
    }

    public void EnableSprint()
    {
        sprintFactor = 2f;
        dValue = 10;
    }

    private void UpdateStamina(bool decreasing)
    {
        if (staminaDecreasing && stamina > 0)
        {
            stamina -= dValue * Time.deltaTime;
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
