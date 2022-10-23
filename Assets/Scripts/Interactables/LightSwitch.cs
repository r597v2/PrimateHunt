using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Interactable

{
    public GameObject lights;

    // Start is called before the first frame update
    void Start()
    {
        lights.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void Interact()
    {
        gameObject.GetComponent<AudioSource>().Play();

        if (lights.activeInHierarchy)
        {
            lights.SetActive(false);
        }
        else
        {
            lights.SetActive(true);
        }
    }
}