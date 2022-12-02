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
    public GameObject armR;
    public GameObject armL;

    public PlayerMovement playerMove;
    public Slider sensSlider;
    public Slider fovSlider;

    public float mouseSensitivity = 100f;
    float xRotation = 0f;
    float yRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        sensSlider.value = 100f;
        cam.fieldOfView = 95f;
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

            if (playerMove.currentAnimController == "ak")
            {
                armL.transform.localRotation = Quaternion.Euler(0f, xRotation, 0f);
                armR.transform.localRotation = Quaternion.Euler(0f, xRotation, 0f);
                Debug.Log("L = " + armL.transform.localRotation+ ", R = " + armR.transform.localRotation);
            }
            else if (playerMove.currentAnimController == "default")
            {
                armL.transform.localRotation = Quaternion.Euler(19.774f, 82.559f, -96.488f);
                armL.transform.localRotation = Quaternion.Euler(19.774f, -73.663f, 98.553f);
            }
        }
    }

    public void ResetDefault()
    {
        sensSlider.value = 100f;
        fovSlider.value = 95f;
    }
}