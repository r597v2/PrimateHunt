using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void GameStart()
    {
        // if (BankIsSelected.bankIsSelected)
        // {
        //     // OpenBankScene();
        // }

        if (BankIsSelected.bankIsSelected && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Disconnect();
            PhotonNetwork.JoinRandomOrCreateRoom();
            PhotonNetwork.LoadLevel(1);
        }
    }

    // public void OpenBankScene()
    // {
    //     SceneManager.LoadScene("Bank");

    //     SceneManager.UnloadSceneAsync("Lobby");
    // }
}
