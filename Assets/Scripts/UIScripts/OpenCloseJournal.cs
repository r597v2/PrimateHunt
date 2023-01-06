using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseJournal : MonoBehaviour
{
    public GameObject journalUI;

    void Start()
    {
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!journalUI.activeInHierarchy)
            {
                journalUI.SetActive(true);
                gameObject.GetComponent<PlayerMovement>().enabled = false;
                gameObject.GetComponent<MouseLook>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                journalUI.SetActive(false);
                gameObject.GetComponent<PlayerMovement>().enabled = true;
                gameObject.GetComponent<MouseLook>().enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
