using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanDoors : Interactable
{
    private string trigger;

    void Start()
    {
        trigger = "Open";
    }

    protected override void Interact(int photonViewID)
    {
        GetComponent<Animator>().SetTrigger(trigger);
        if(trigger == "Open") trigger = "Close";
        else trigger = "Open";
    }
}
