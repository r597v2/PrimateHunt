using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : Interactable
{
    public GameObject levelSelectUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (levelSelectUI.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {
            levelSelectUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    
    protected override void Interact(int photonViewID)
    {
        if (!levelSelectUI.activeInHierarchy)
        {
            levelSelectUI.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
