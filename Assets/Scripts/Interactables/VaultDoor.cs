using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultDoor : Interactable
{
    public Animator vaultAnimator;
    private bool isOpen;

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
        if (!isOpen)
        {
            vaultAnimator.SetTrigger("TrOpenVault");
            isOpen = true;
        }
        else
        {
            vaultAnimator.SetTrigger("TrCloseVault");
            isOpen = false;
        }
    }
}
