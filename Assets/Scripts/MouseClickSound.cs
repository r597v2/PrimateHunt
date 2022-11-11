using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickSound : MonoBehaviour
{
    public GameObject mouseClick;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MouseClick()
    {
        mouseClick.GetComponentInChildren<AudioSource>().Play();
    }
}
