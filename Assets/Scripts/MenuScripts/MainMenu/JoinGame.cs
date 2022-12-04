using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;


public class JoinGame : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI roomName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JoinOnClick()
    {
        PhotonNetwork.JoinRoom(roomName.text);
        SceneManager.LoadScene("Lobby");
    }
}
