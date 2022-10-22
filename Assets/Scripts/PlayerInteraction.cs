using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerInteraction : MonoBehaviourPunCallbacks
{
    private string switchName;

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

        if (Input.GetMouseButtonDown(0) && switchName != null)
        {
            photonView.RPC("RPC_ToggleLights", RpcTarget.AllBuffered, switchName);
        }
    }

    [PunRPC]
    private void RPC_ToggleLights(string switchName)
    {
        // find the light switch with name "switchName"
        LightSwitch lightSwitch = null;
        LightSwitch[] allLightSwitches = FindObjectsOfType<LightSwitch>();
        foreach(LightSwitch lsw in allLightSwitches)
        {
            if(lsw.switchName == switchName)
            {
                lightSwitch = lsw;
                break;
            }
        }

        lightSwitch?.Toggle();
    }

    public void SetLightSwitchName(string switchName)
    {
        this.switchName = switchName;
    }
}
