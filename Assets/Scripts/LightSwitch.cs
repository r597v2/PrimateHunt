using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public GameObject lightObj;

    public string switchName = "LOBBY";

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
        playerAtSwitch?.SetLightSwitchName(switchName);
    }

    void OnBecameInvisible()
    {
        isVisible = false;
        playerAtSwitch?.SetLightSwitchName(null);
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

            playerAtSwitch.SetLightSwitchName(isVisible ? switchName : null);
        }
     }
    

    private void OnTriggerExit(Collider other)     //if player exit zone
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerInteraction>().SetLightSwitchName(null);
            playerAtSwitch = null;
        }
    }
}