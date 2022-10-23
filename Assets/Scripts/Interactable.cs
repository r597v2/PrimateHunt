using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //message displayed to the player when looking at an interactable
    public string promptMessage;
    
    public void BaseIntereract()
    {
        Interact();
    }

    protected virtual void Interact()
    {
        //template function to be overridden by subclasses
    }
}
