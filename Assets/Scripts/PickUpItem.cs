using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : Interactable
{
    public InventoryItemData ItemData;

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
        GameObject playerGameObject = FindPlayerGameObjectByPhotonViewID(photonViewID);
        var inventory = playerGameObject?.transform.GetComponent<InventoryHolder>();

        if (!inventory) return;

        if (inventory.InventorySystem.AddToInventory(ItemData, 1))
        {
            Destroy(this.gameObject);
        }

    }
}
