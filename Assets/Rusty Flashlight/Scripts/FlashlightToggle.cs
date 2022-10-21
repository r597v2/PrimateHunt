using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using Photon.Pun;

public class FlashlightToggle : MonoBehaviourPunCallbacks
{

    public GameObject lightGO; //light gameObject to work with
    private bool isOn = false; //is flashlight on or off?

    // Use this for initialization
    void Start()
    {
        //set default off
        lightGO.SetActive(isOn);

        if (photonView.IsMine)
        {
            photonView.RPC("RPC_FlashlightControl", RpcTarget.AllBuffered, isOn);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) {
            return;
        }

        //toggle flashlight on key down
        if (Input.GetMouseButton(1))
        {
            //toggle light
            isOn = !isOn;
            photonView.RPC("RPC_FlashlightControl", RpcTarget.AllBuffered, isOn);
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

    [PunRPC]
    private void RPC_FlashlightControl(bool isOn)
    {
        //turn light on
        if (isOn)
        {
            lightGO.SetActive(true);
        }
        //turn light off
        else
        {
            lightGO.SetActive(false);
        }
    }
}
