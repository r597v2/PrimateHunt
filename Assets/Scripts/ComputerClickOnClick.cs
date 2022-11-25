using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerClickOnClick : MonoBehaviour
{
    public GameObject mouse;
    public GameObject levelSelectUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (levelSelectUI.activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouse.GetComponent<AudioSource>().Play();
            }
        }
    }
}
