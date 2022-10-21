using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public GameObject lightObj;

    private PlayerInteraction playerAtSwitch;

    private bool isVisible;

    private void Start()
    {
    }

    private void Update()
    {
    }

    void OnBecameVisible()
    {
        isVisible = true;
        playerAtSwitch?.SetLightSwitch(this);
    }

    void OnBecameInvisible()
    {
        isVisible = false;
        playerAtSwitch?.SetLightSwitch(null);
    }

    public void Toggle()
    {
        lightObj.SetActive(!lightObj.activeSelf);
        gameObject.GetComponent<AudioSource>().Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")     //if player in zone
        {
            playerAtSwitch = other.gameObject.GetComponent<PlayerInteraction>();

            playerAtSwitch.SetLightSwitch(isVisible ? this : null);
        }
     }
    

    private void OnTriggerExit(Collider other)     //if player exit zone
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerInteraction>().SetLightSwitch(null);
            playerAtSwitch = null;
        }
    }
}