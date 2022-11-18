using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeRight : Interactable
{
    private bool fridgeOpen;
    public Animator fridgeRightAnimator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (fridgeOpen)
        {
            promptMessage = ("Press LMB to close");
        }
        else
        {
            promptMessage = ("Press LMB to open");
        }
    }

    protected override void Interact(int photonViewID)
    {
        if (!fridgeOpen)
        {
            fridgeRightAnimator.SetBool("FridgeOpen", true);
            fridgeOpen = true;
        }
        else
        {
            fridgeRightAnimator.SetBool("FridgeOpen", false);
            fridgeOpen = false;
        }
    }
}
