using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    private Animator mAnimator;

    public GameObject handCollider;
    public GameObject headCollider;

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
    private bool sprintDisabled;

    public float gravity = -9.81f;
    public float groundDistance = 0.1f;
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundMask;
    public GameObject bananaEat;

    public CharacterController playerHeight;
    public CharacterController playerCenter;
    private bool isCrouched = false;
    private bool staminaDecreasing;

    Vector3 velocity;
    public Rigidbody mRigidbody;
    private bool isMoving;

    bool slapIsPlaying(Animator mAnimator, string Slap)
    {
        if (mAnimator.GetCurrentAnimatorStateInfo(0).IsTag(Slap) &&
        mAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    bool headbuttIsPlaying(Animator mAnimator, string Headbutt)
    {
        if (mAnimator.GetCurrentAnimatorStateInfo(0).IsTag(Headbutt) &&
        mAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Start()
    {
        speed = normalSpeed;
        maxStamina = stamina;
        staminaBar.maxValue = maxStamina;
        mAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            // isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            // if (isGrounded && velocity.y < 0)
            // {
            //     Debug.Log("Grounded");
            // }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            isMoving = (x != 0f || z != 0f);

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

            if (mAnimator != null)
            {
                // Check crouching
                if (Input.GetKeyDown(KeyCode.C))
                {
                    if (!isCrouched)
                    {
                        Crouch();
                    }
                    else
                    {
                        UnCrouch();
                    }
                }
            }

            // Move
            controller.Move(move * speed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);

            //eating bananas 
            if (Input.GetKeyDown(KeyCode.B))
            {
                EatBanana();
            }

            CheckAnimateMoving();
            CheckSprintDisabled();
            CheckSprintWithoutMoving();
            UpdateStamina(staminaDecreasing);
        }
    }

    public void CheckAnimateMoving()
    {
        if (!isCrouched)
        {
            if (Input.GetKey(KeyCode.W))
            {
                mAnimator.SetBool(staminaDecreasing && Input.GetKey(KeyCode.LeftShift) ? "BoSprinting" : "BoWalking", true);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                mAnimator.SetBool("BoSprinting", false);
                mAnimator.SetBool("BoWalking", false);
            }

            if (Input.GetKey(KeyCode.S))
            {
                mAnimator.SetBool(staminaDecreasing && Input.GetKey(KeyCode.LeftShift) ? "BoSprintingBack" : "BoWalkingBack", true);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                mAnimator.SetBool("BoSprintingBack", false);
                mAnimator.SetBool("BoWalkingBack", false);
            }

            if (Input.GetKey(KeyCode.A))
            {
                mAnimator.SetBool(staminaDecreasing && Input.GetKey(KeyCode.LeftShift) ? "BoSprintingLeft" : "BoWalkingLeft", true);
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                mAnimator.SetBool("BoSprintingLeft", false);
                mAnimator.SetBool("BoWalkingLeft", false);
            }

            if (Input.GetKey(KeyCode.D))
            {
                mAnimator.SetBool(staminaDecreasing && Input.GetKey(KeyCode.LeftShift) ? "BoWalkingRight" : "BoWalkingRight", true);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                mAnimator.SetBool("BoSprintingRight", false);
                mAnimator.SetBool("BoWalkingRight", false);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                mAnimator.SetBool(staminaDecreasing && Input.GetKey(KeyCode.LeftShift) ? "BoCrouchSprinting" : "BoCrouchWalking", true);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                mAnimator.SetBool("BoCrouchSprinting", false);
                mAnimator.SetBool("BoCrouchWalking", false);
            }

            if (Input.GetKey(KeyCode.S))
            {
                mAnimator.SetBool(staminaDecreasing && Input.GetKey(KeyCode.LeftShift) ? "BoCrouchSprintingBack" : "BoCrouchWalkingBack", true);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                mAnimator.SetBool("BoCrouchSprintingBack", false);
                mAnimator.SetBool("BoCrouchWalkingBack", false);
            }

            if (Input.GetKey(KeyCode.A))
            {
                mAnimator.SetBool(staminaDecreasing && Input.GetKey(KeyCode.LeftShift) ? "BoCrouchSprintingLeft" : "BoCrouchWalkingLeft", true);
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                mAnimator.SetBool("BoCrouchSprintingLeft", false);
                mAnimator.SetBool("BoCrouchWalkingLeft", false);
            }

            if (Input.GetKey(KeyCode.D))
            {
                mAnimator.SetBool(staminaDecreasing && Input.GetKey(KeyCode.LeftShift) ? "BoCrouchSprintingRight" : "BoCrouchWalkingRight", true);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                mAnimator.SetBool("BoCrouchSprintingRight", false);
                mAnimator.SetBool("BoCrouchWalkingRight", false);
            }
        }

        // if (!slapIsPlaying(mAnimator, "Slap"))
        // {
        //     handCollider.SetActive(false);
        // }
        // else
        // {
        //     handCollider.SetActive(true);
        // }

        // if (!slapIsPlaying(mAnimator, "CrouchSlap"))
        // {
        //     handCollider.SetActive(false);
        // }
        // else
        // {
        //     handCollider.SetActive(true);
        // }

        // if (!headbuttIsPlaying(mAnimator, "Headbutt"))
        // {
        //     headCollider.SetActive(false);
        // }
        // else
        // {
        //     headCollider.SetActive(true);
        // }

        // if (!headbuttIsPlaying(mAnimator, "CrouchHeadbutt"))
        // {
        //     headCollider.SetActive(false);
        // }
        // else
        // {
        //     headCollider.SetActive(true);
        // }
    }

    public void Slap()
    {
        mAnimator.SetTrigger("TrSlap");
    }

    public void Headbutt()
    {
        mAnimator.SetTrigger("TrHeadbutt");
    }

    private void CheckSprintWithoutMoving()
    {
        if (isSprinting)
        {
            if (isMoving)
            {
                dValue = 10f;
            }
            else
            {
                dValue = 0f;
            }
        }
    }

    private void CheckSprintDisabled()
    {
        if (sprintDisabled)
        {
            dValue = 0f;
        }
        else
        {
            dValue = 10f;
        }
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
        mAnimator.SetBool("BoSprinting", false);
        mAnimator.SetBool("BoSprintingBack", false);
        mAnimator.SetBool("BoSprintingRight", false);
        mAnimator.SetBool("BoSprintingLeft", false);
    }

    private void DisableSprint()
    {
        if (isSprinting)
        {
            StopSprinting();
        }

        sprintFactor = 1f;
        dValue = 0;
        sprintDisabled = true;
    }

    public void EnableSprint()
    {
        sprintFactor = 2f;
        dValue = 10;
        sprintDisabled = false;
    }

    public void UpdateStamina(bool decreasing)
    {
        if (staminaDecreasing && stamina > 0)
        {
            stamina -= dValue * Time.deltaTime;
            staminaBar.value = stamina;
        }
    }

    private void Crouch()
    {
        mAnimator.SetTrigger("TrCrouch");
        speed *= crouchFactor;
        controller.height = 1.04f;
        controller.center = new Vector3(0f, 0.56f, 0.11f);
        isCrouched = true;
    }

    private void UnCrouch()
    {
        mAnimator.SetTrigger("TrUnCrouch");
        Debug.Log("Uncrouch");
        speed /= crouchFactor;
        controller.height = 1.6f;
        controller.center = new Vector3(0f, 0.83f, -0.04f);
        isCrouched = false;
    }

    private void EatBanana()
    {
        InventoryItemData itemData = new InventoryItemData(InventoryItemData.ITEMTYPES.BANANA);
        InventoryHolder ih = GetComponent<InventoryHolder>();

        if (ih.InventorySystem.RetrieveFromInventory(itemData, 1))
        {
            bananaEat.GetComponent<AudioSource>().Play();

            if (stamina >= maxStamina)
            {
                stamina -= dValue * obesity; //remove dValue * obesity from stamina
                transform.Find("Monkey/metarig/spine/spine.001/spine.002/spine.003/spine.004/spine.006/spine.006_end/Head/VomitControl").GetComponent<VomitControl>().Vomit(); //vomit
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
}
