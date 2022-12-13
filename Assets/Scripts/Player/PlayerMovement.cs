using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    public Camera cam;

    public Animator mAnimator;
    public RuntimeAnimatorController defaultController;
    public RuntimeAnimatorController akController;
    public string currentAnimController = "default";

    public GameObject handCollider;
    public GameObject headCollider;

    public GameObject armL;
    public GameObject armR;
    public GameObject head;

    public CharacterController controller;
    public GameObject akGO;
    public GameObject akDropGO;
    public PhotonView akDropPrefab;
    public GameObject weaponCrosshair;

    public GameObject c4GO;
    public GameObject c4DropGO;
    public PhotonView c4DropPrefab;
    private float throwForce = 3f;

    private Slider staminaBar;
    public float dValue;
    public float obesity;
    public float stamina;
    float maxStamina;

    public Slider healthBar;
    public float health;
    float maxHealth;

    public float normalSpeed = 5f;
    private float speed;
    private float sprintFactor = 2f;
    public bool isSprinting;
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
    public bool isCrouched = false;
    private bool staminaDecreasing;

    Vector3 velocity;
    public Rigidbody mRigidbody;
    private bool isMoving;

    void Start()
    {
        if (photonView.IsMine)
        {
            staminaBar = (Slider)GameObject.FindObjectsOfType(typeof(Slider))[1];
            healthBar = (Slider)GameObject.FindObjectsOfType(typeof(Slider))[0];

            speed = normalSpeed;
            maxStamina = stamina;
            staminaBar.maxValue = maxStamina;

            maxHealth = healthBar.maxValue;
            health = maxHealth;
        }
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

            healthBar.value = health;

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

        if (currentAnimController == "ak")
        {
            weaponCrosshair.SetActive(true);
        }
        else
        {
            weaponCrosshair.SetActive(false);
        }
    }

    public void SetDefaultAnimations()
    {
        mAnimator.SetLayerWeight(1, 0f);
        mAnimator.runtimeAnimatorController = defaultController as RuntimeAnimatorController;
        currentAnimController = "default";
        akGO.SetActive(false);
        c4GO.SetActive(false);
    }

    public void SetAKAnimations()
    {
        mAnimator.runtimeAnimatorController = akController as RuntimeAnimatorController;
        currentAnimController = "ak";
        akGO.SetActive(true);
        c4GO.SetActive(false);

    }

    public void SetC4Animations()
    {
        mAnimator.runtimeAnimatorController = defaultController as RuntimeAnimatorController;
        mAnimator.SetLayerWeight(1, 1f);
        currentAnimController = "c4";
        c4GO.SetActive(true);
        akGO.SetActive(false);
    }

    public void DropAk()
    {
        akDropGO = PhotonNetwork.Instantiate(akDropPrefab.name, cam.transform.position, cam.transform.rotation);
        akDropGO.GetComponent<Rigidbody>().AddForceAtPosition(cam.transform.forward * 10f, cam.transform.position,
         ForceMode.Impulse);
        akGO.SetActive(false);
        InventoryHolder ih = GetComponent<InventoryHolder>();
        InventoryItemData itemData = new InventoryItemData(InventoryItemData.ITEMTYPES.DEFAULT);

        ih.InventorySystem.RemoveFromInventory(itemData, 1);
    }

    public void DropC4()
    {
        c4DropGO = PhotonNetwork.Instantiate(c4DropPrefab.name, cam.transform.position, cam.transform.rotation);
        c4DropGO.GetComponent<Rigidbody>().AddForceAtPosition(cam.transform.forward * 5f, cam.transform.position,
         ForceMode.Impulse);
        c4DropGO.GetComponent<C4PickUpItem>().armed = false;
        c4GO.SetActive(false);
        InventoryHolder ih = GetComponent<InventoryHolder>();
        InventoryItemData itemData = new InventoryItemData(InventoryItemData.ITEMTYPES.DEFAULT);

        ih.InventorySystem.RemoveFromInventory(itemData, 1);
    }

    public void AimC4()
    {
        mAnimator.SetBool("BoThrowing", true);
        throwForce += Time.deltaTime * 2;
    }

    public void ThrowC4()
    {
        c4DropGO = PhotonNetwork.Instantiate(c4DropPrefab.name, cam.transform.position, cam.transform.rotation);
        c4DropGO.GetComponent<Rigidbody>().AddForceAtPosition(cam.transform.forward * throwForce, cam.transform.position,
         ForceMode.Impulse);
        c4GO.SetActive(false);

        c4DropGO.GetComponent<C4PickUpItem>().armed = true;

        InventoryHolder ih = GetComponent<InventoryHolder>();
        InventoryItemData itemData = new InventoryItemData(InventoryItemData.ITEMTYPES.DEFAULT);

        ih.InventorySystem.RemoveFromInventory(itemData, 1);

        mAnimator.SetBool("BoThrowing", false);
        throwForce = 3f;
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
            Debug.Log(Time.time + "staminaBar.value=" + staminaBar.value);
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
        speed /= crouchFactor;
        controller.height = 1.6f;
        controller.center = new Vector3(0f, 0.83f, -0.04f);
        isCrouched = false;
    }

    private void EatBanana()
    {
        InventoryItemData bananaItemData = new InventoryItemData(InventoryItemData.ITEMTYPES.BANANA);
        InventoryHolder ih = GetComponent<InventoryHolder>();

        if (ih.InventorySystem.RetrieveFromInventory(bananaItemData, 1))
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
                if ((stamina += dValue) > maxStamina)
                {
                    EnableSprint();
                    stamina = maxStamina;
                    staminaBar.value = stamina;
                }
                else
                {
                    EnableSprint();
                    stamina += dValue;
                    staminaBar.value = stamina;
                }
            }
        }

    }
}
