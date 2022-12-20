using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviourPunCallbacks
{
    public PhotonView playerPrefab;
    public Scene bank;
    SpawnSpot[] spawnSpots;

    private int numOfItemSpawnSpots;

    ItemSpawnSpot[] itemSpawnSpots;
    public List<GameObject> spawnableItems = new List<GameObject>();

    private float[] difficultyFactor = new float[] { 1f, 0.8f, 0.6f, 0.4f };

    // Start is called before the first frame update
    void Start()
    {
        spawnSpots = GameObject.FindObjectsOfType<SpawnSpot>();
        itemSpawnSpots = GameObject.FindObjectsOfType<ItemSpawnSpot>();
        numOfItemSpawnSpots = (int)(itemSpawnSpots.Length * difficultyFactor[GlobalGameSettings.difficultyLevel]);

        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            spawnPlayer();
            spawnItems();
        }
        else
            // {
            //     PhotonNetwork.ConnectUsingSettings();
            // }

            PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        // PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created a new room.");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogErrorFormat("Room creation failed with error code {0} and error message {1}", returnCode, message);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Warning: failed to join room. " + message);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("New player joined: " + newPlayer.ToStringFull());
        Debug.Log("Players in this room: " + PhotonNetwork.CurrentRoom.PlayerCount + ". Players in all rooms: " + PhotonNetwork.PlayerList.Length);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("A player left");
        Debug.Log("Players in this room: " + PhotonNetwork.CurrentRoom.PlayerCount + ". Players in all rooms: " + PhotonNetwork.PlayerList.Length);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Successfully Joined a Room.");
        Debug.Log("Players in this room: " + PhotonNetwork.CurrentRoom.PlayerCount + ". Players in all rooms: " + PhotonNetwork.PlayerList.Length);

        spawnPlayer();
        spawnItems();
    }

    public void spawnPlayer()
    {
        SpawnSpot mySpawnSpot = spawnSpots[Random.Range(0, spawnSpots.Length)];
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);

        player.transform.Find("Monkey/metarig/spine/spine.001/spine.002/spine.003/spine.004/spine.006/spine.006_end/Head/Camera").gameObject.SetActive(true);
        player.transform.Find("Canvas").gameObject.SetActive(true);
    }

    public void spawnItems()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // foreach (var itemSpawnSpot in itemSpawnSpots)
            for (int i = 0; i < numOfItemSpawnSpots; i++)
            {
                GameObject randomItem = Instantiate(spawnableItems[Random.Range(0, spawnableItems.Count)],
                    transform.position, Quaternion.identity, itemSpawnSpots[i].transform);

                randomItem.transform.localPosition = Vector3.zero;

                if (randomItem.tag == "Ak47SpawnItem" || randomItem.tag == "AmmoSpawnItem")
                {
                    randomItem.transform.localRotation = Quaternion.Euler(0, -90f, 0);
                }
                else if (randomItem.tag == "AxeSpawnItem")
                {
                    randomItem.transform.localRotation = Quaternion.Euler(-90f, -90f, 0);
                }
            }
        }
    }
}
