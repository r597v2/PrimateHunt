using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteboardFlip : Interactable
{
    private Animator controller;
    private bool flipped;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Animator>();
    }

    protected override void Interact(int photonViewID)
    {
        if (!flipped)
        {
            controller.SetTrigger("Flip");
            flipped = true;
        }
        else
        {
            controller.SetTrigger("UnFlip");
            flipped = false;
        }
    }
}
