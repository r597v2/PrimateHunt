using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PauseMenu : MonoBehaviourPunCallbacks
{
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject optionsButton;
    public GameObject playerObj;
    public GameObject evidenceJournal;

    private bool gamePaused;
    private bool optionsOpen;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!gamePaused)
                {
                    OpenPauseMenu();
                }
                else
                {
                    if (optionsOpen)
                    {
                        CloseOptions();
                    }
                    else
                    {
                        CloseMenu();
                    }
                }
            }
        }
    }

    public void CheckMenus()
    {
        if (photonView.IsMine)
        {
            if (gamePaused)
            {
                if (optionsOpen)
                {
                    CloseOptions();
                }
                else if (!optionsOpen)
                {
                    CloseMenu();
                }
            }
        }
    }

    public void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        playerObj.GetComponent<MouseLook>().enabled = false;
        playerObj.GetComponent<PlayerMovement>().enabled = false;
        gamePaused = true;
        gameObject.GetComponent<OpenCloseJournal>().enabled = false;
        evidenceJournal.SetActive(false);
    }

    public void CloseMenu()
    {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        playerObj.GetComponent<MouseLook>().enabled = true;
        playerObj.GetComponent<PlayerMovement>().enabled = true;
        gamePaused = false;
        gameObject.GetComponent<OpenCloseJournal>().enabled = true;
    }

    public void OpenOptions()
    {
        if (photonView.IsMine)
        {
            optionsButton.SetActive(false);
            optionsMenu.SetActive(true);
            optionsOpen = true;
        }
    }

    public void CloseOptions()
    {
        optionsButton.SetActive(true);
        optionsMenu.SetActive(false);
        optionsOpen = false;
    }
}
