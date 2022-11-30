using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject optionsButton;
    public GameObject playerObj;

    private bool gamePaused;
    private bool optionsOpen;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gamePaused)
            {
                OpenPauseMenu();
            }
            else
            {
                if (!optionsOpen)
                {
                    CloseMenu();
                }
                else
                {
                    CloseOptions();
                }
            }
        }
    }

    public void CheckMenus()
    {
        if (gamePaused)
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

    public void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        playerObj.GetComponent<MouseLook>().enabled = false;
        playerObj.GetComponent<PlayerMovement>().enabled = false;
        gamePaused = true;
    }

    public void CloseMenu()
    {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        playerObj.GetComponent<MouseLook>().enabled = true;
        playerObj.GetComponent<PlayerMovement>().enabled = true;
        gamePaused = false;
    }

    public void OpenOptions()
    {
        optionsButton.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsButton.SetActive(true);
        optionsMenu.SetActive(false);
    }
}
