using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MouseLook : MonoBehaviourPunCallbacks
{
    public Camera cam;
    public GameObject head;
    public GameObject playerModel;
    public PauseMenu pauseMenu;

    public PlayerMovement playerMove;
    public Animator playerAnimator;
    public Slider sensSlider;
    public Slider fovSlider;
    public GameObject armL;
    public GameObject armR;

    public float mouseSensitivity = 100f;
    float xRotation = 0f;
    float yRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Awake()
    {
        ResetDefault();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            yRotation += mouseX;

            playerModel.transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
            head.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            mouseSensitivity = sensSlider.value;
            cam.fieldOfView = fovSlider.value;

            if (sensSlider.value == 0)
            {
                sensSlider.value = 1;
            }

            if (playerMove.currentAnimController == "default")
            {
                playerModel.GetComponentInChildren<Animator>().enabled = true;
            }
            else if (playerMove.currentAnimController == "ak")
            {
                playerAnimator.SetFloat("FlLookValue", xRotation/(-90.0f), 0f, 0f);
            }
            if (playerMove.currentAnimController == "c4")
            {
                playerAnimator.SetFloat("FlC4LookValue", xRotation/(-90.0f), 0f, 0f);
            }
        }
    }

    public void ResetDefault()
    {
        sensSlider.value = 100f;
        fovSlider.value = 95f;
        pauseMenu.ResetDefaults();
    }
}