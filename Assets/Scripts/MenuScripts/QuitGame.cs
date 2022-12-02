using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.Threading;

public class QuitGame : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PhotonNetwork.CountOfRooms + " rooms currently active.");
        Debug.Log("Currently in room: " + PhotonNetwork.CurrentRoom.Name);
    }

    public void QuitGameToMenu()
    {
        SceneManager.LoadScene("Menu");
        PhotonNetwork.LeaveRoom();
    }

    // public override void OnLeftRoom()
    // {
    //     Debug.Log("Left room.");
    //     // PhotonNetwork.Disconnect();
    // }

    // public override void OnDisconnected(DisconnectCause cause)
    // {
    //     Debug.Log("Loading menu.");
    //     SceneManager.LoadScene("Menu");
    // }
}
