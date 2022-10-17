using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using Photon.Pun;

public class FlashlightToggle : MonoBehaviourPunCallbacks
{

    public GameObject lightGO; //light gameObject to work with
    private bool isOn = false; //is flashlight on or off?
    private PhotonView PV;

    // Use this for initialization
    void Start()
    {
        //set default off
        lightGO.SetActive(isOn);

        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            PV.RPC("RPC_FlashlightControl", RpcTarget.AllBuffered, isOn);
        }
    }

    // Update is called once per frame
    void Update()
    {
        RPC_FlashlightControl();
    }

    [PunRPC]
    private void RPC_FlashlightControl()
    {
        if (photonView.IsMine)
        {
            //toggle flashlight on key down
            if (Input.GetMouseButton(1))
            {
                //toggle light
                isOn = !isOn;
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
    }
}
