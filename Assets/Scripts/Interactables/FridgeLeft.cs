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

    }

    protected override void Interact(int photonViewID)
    {
        if (!fridgeOpen)
        {
            fridgeLeftAnimator.SetTrigger("OpenFridge");
            fridgeOpen = true;
        }
        else
        {
            fridgeLeftAnimator.SetTrigger("CloseFridge");
            fridgeOpen = false;
        }
    }
}
