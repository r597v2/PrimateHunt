using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseLevelSelect : MonoBehaviour
{
    public GameObject levelSelectUI;
    public GameObject playerObj;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CloseUI()
    {
        levelSelectUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        playerObj.GetComponent<MouseLook>().enabled = true;
        playerObj.GetComponent<PlayerMovement>().enabled = true;
    }
}
