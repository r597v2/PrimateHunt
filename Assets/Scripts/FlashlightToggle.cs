using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using Photon.Pun;

public class FlashlightToggle : MonoBehaviourPunCallbacks
{

    public GameObject lightGO; //light gameObject to work with
    public GameObject lightBlock;
    public PlayerMovement playerMvt;
    private bool isOn = false; //is flashlight on or off?

    // Use this for initialization
    void Start()
    {
        //set default off
        lightGO.SetActive(isOn);
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (playerMvt.currentAnimController == "default")
        {
            //toggle flashlight on key down
            if (Input.GetMouseButton(1))
            {
                //toggle light
                isOn = !isOn;
                photonView.RPC("RPC_FlashlightControl", RpcTarget.AllBuffered, isOn);
            }
        }
        else if (playerMvt.currentAnimController == "ak")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                //toggle light
                isOn = !isOn;
                photonView.RPC("RPC_FlashlightControl", RpcTarget.AllBuffered, isOn);
            }
        }
    }

    [PunRPC]
    private void RPC_FlashlightControl(bool isOn)
    {
        gameObject.GetComponent<AudioSource>().Play();

        //turn light on
        if (isOn)
        {
            lightGO.SetActive(true);
            lightBlock.SetActive(false);
        }
        //turn light off
        else
        {
            lightGO.SetActive(false);
            lightBlock.SetActive(true);
        }
    }
}