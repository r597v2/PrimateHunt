using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Interactable

{
    public GameObject lights;

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
        gameObject.GetComponent<AudioSource>().Play();

        lights.SetActive(!lights.activeInHierarchy);
    }
}