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

        //toggle flashlight on key down
        if (Input.GetKeyDown(KeyCode.G))
        {
            //toggle light
            isOn = !isOn;
            photonView.RPC("RPC_FlashlightControl", RpcTarget.AllBuffered, isOn);
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
            lightBlock.SetActive(true);
        }
        //turn light off
        else
        {
            lightGO.SetActive(false);
            lightBlock.SetActive(false);
        }
    }
}