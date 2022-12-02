using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public GameObject createRoom;
    public GameObject roomListings;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenCreateLobby()
    {
        createRoom.SetActive(true);
        roomListings.SetActive(true);
        gameObject.SetActive(false);
    }
}
