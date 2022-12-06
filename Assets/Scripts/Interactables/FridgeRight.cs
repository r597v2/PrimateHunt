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
        
    }

    protected override void Interact(int photonViewID)
    {
        if (!fridgeOpen)
        {
            fridgeRightAnimator.SetTrigger("OpenFridge");
            fridgeOpen = true;
        }
        else
        {
            fridgeRightAnimator.SetTrigger("CloseFridge");
            fridgeOpen = false;
        }
    }
}
