using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class VomitControl : MonoBehaviourPunCallbacks
{
    public ParticleSystem vomitParticles;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Vomit();
        }
    }

    [PunRPC]
    public void RPC_VomitControl()
    {
        vomitParticles.Play();
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void Vomit()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("RPC_VomitControl", RpcTarget.AllBuffered);
        }
    }
}
