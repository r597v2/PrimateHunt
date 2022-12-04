using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : Interactable
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

    protected override void Interact(int photonViewID)
    {
        levelSelectUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
}
