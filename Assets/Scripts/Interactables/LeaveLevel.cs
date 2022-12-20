using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class LeaveLevel : Interactable
{
    protected override void Interact(int photonViewID)
    {
        
    }

    public void CloseComfirmUI()
    {
        
    }

    public void LeaveLevelOnClick()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            SceneManager.LoadScene("Lobby");
        }
    }
}
