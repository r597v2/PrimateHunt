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
        if (levelSelectUI.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseUI();
        }
    }

    public void CloseUI()
    {
        levelSelectUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
