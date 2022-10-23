using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class Interactable : MonoBehaviourPun
{
    //message displayed to the player when looking at an interactable
    public string promptMessage;
    // Name used by RPC
    public string idName = "DEFAULT";
    
    public void BaseIntereract()
    {
        photonView?.RPC("RPC_Interact", RpcTarget.AllBuffered, idName);
    }

    [PunRPC]
    protected void RPC_Interact(string idName)
    {
        Interactable target = null;
        Interactable[] allInteractables = FindObjectsOfType<Interactable>();
        foreach(Interactable interactable in allInteractables)
        {
            if(interactable.idName == idName)
            {
                target = interactable;
                break;
            }
        }

        target?.Interact();
    }

    protected virtual void Interact()
    {
        //template function to be overridden by subclasses
    }
}
