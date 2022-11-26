using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Interactable

{
    public GameObject lights;
    public Animator lightAnimator;
    private bool lightsOn;

    // Start is called before the first frame update
    void Start()
    {
        lightsOn = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void Interact(int photonViewID)
    {
        gameObject.GetComponent<AudioSource>().Play();

        if (lightsOn)
        {
            lightAnimator.SetTrigger("TrOff");
            lights.SetActive(false);
            lightsOn = false;
        }
        else
        {
            lightAnimator.SetTrigger("TrOn");
            lights.SetActive(true);
            lightsOn = true;
        }
    }
}