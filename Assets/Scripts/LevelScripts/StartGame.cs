using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviourPunCallbacks
{
    public LevelIsSelected levelSelectedScript;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void GameStart()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            SceneManager.LoadScene("Bank");
        }
    }
}
