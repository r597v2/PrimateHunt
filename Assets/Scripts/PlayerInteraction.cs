using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerInteraction : MonoBehaviourPunCallbacks
{
    private LightSwitch lightSwitch;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && lightSwitch != null)
        {
            photonView.RPC("RPC_ToggleLights", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    private void RPC_ToggleLights()
    {
        lightSwitch?.Toggle();
    }

    public void SetLightSwitch(LightSwitch lightSwitch)
    {
        this.lightSwitch = lightSwitch;
    }

    // private void OnTriggerEnter(Collider other)
    // {
    // if (other.gameObject.tag == "Item")
    // {
    //     Destroy(other.gameObject);
    //     Debug.Log("banan get");
    // }
    // }
}
