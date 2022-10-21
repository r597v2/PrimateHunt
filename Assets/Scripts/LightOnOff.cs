using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnOff : MonoBehaviour
{
    public GameObject lightObj;

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void toggle()
    {
        lightObj.SetActive(!lightObj.activeSelf);
        gameObject.GetComponent<AudioSource>().Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")     //if player in zone
        {
            other.gameObject.GetComponent<PlayerInteraction>().SetLightOnOff(this);
        }
     }
    

    private void OnTriggerExit(Collider other)     //if player exit zone
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerInteraction>().SetLightOnOff(null);
        }
    }
}