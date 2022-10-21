using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerInteraction : MonoBehaviourPunCallbacks
{
    private LightOnOff lightOnOff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) {
            return;
        }

        if (Input.GetMouseButtonDown(0) && lightOnOff != null)
        {
            lightOnOff.toggle();
        }
    }

    public void SetLightOnOff(LightOnOff lightOnOff) 
    {
        this.lightOnOff  = lightOnOff;
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
