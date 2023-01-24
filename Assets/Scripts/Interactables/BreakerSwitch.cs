using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakerSwitch : Interactable
{
    public GameObject lights;
    public GameObject lightOn;
    public GameObject lightOff;

    public bool lightsAreOn;

    // Start is called before the first frame update
    void Start()
    {
        if (lightsAreOn)
        {
            lights.SetActive(true);
            lightOff.SetActive(false);
            lightOn.SetActive(true);
        }
        else
        {
            lights.SetActive(false);
            lightOff.SetActive(true);
            lightOn.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void Interact(int photonViewID)
    {
        gameObject.GetComponent<AudioSource>().Play();
        lights.SetActive(!lights.activeInHierarchy);

        if (lightsAreOn)
        {
            lightsAreOn = false;
            lightOff.SetActive(true);
            lightOn.SetActive(false);
        }
        else
        {
            lightsAreOn = true;
            lightOff.SetActive(false);
            lightOn.SetActive(true);
        }
    }
}
