using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviourPunCallbacks
{
    public BankIsSelected bankSelectedScript;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void GameStart()
    {
        if (bankSelectedScript.bankIsSelected && PhotonNetwork.IsMasterClient)
        {
            SceneManager.LoadScene("Bank");
        }
    }
}
