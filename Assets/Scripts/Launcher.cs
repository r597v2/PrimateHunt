using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{
    public PhotonView playerPrefab;
    SpawnSpot[] spawnSpots;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        spawnSpots = GameObject.FindObjectsOfType<SpawnSpot>();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Successfully Joined a Room");
        SpawnSpot mySpawnSpot = spawnSpots [Random.Range (0, spawnSpots.Length)];
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
        player.transform.Find("Camera").gameObject.SetActive(true);
    }
}
