using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeLeft : Interactable
{
    private bool fridgeOpen;
    public Animator fridgeLeftAnimator;

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
            fridgeLeftAnimator.SetBool("FridgeOpen", true);
            fridgeOpen = true;
        }
        else
        {
            fridgeLeftAnimator.SetBool("FridgeOpen", false);
            fridgeOpen = false;
        }
    }
}
