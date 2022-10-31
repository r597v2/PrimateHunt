using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private InventorySlot_UI[] slots;

    void Awake()
    {
        hideAllSelections();
    }

    protected override void Start()
    {
        base.Start();

        if (inventoryHolder != null)
        {
            inventorySystem = inventoryHolder.InventorySystem;
            inventorySystem.OnInventorySlotChanged += UpdateSlot;
        }
        else Debug.LogWarning($"No inventory assigned to {this.gameObject}");

        AssignSlot(inventorySystem);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            hideAllSelections();
            slots[0].selectionSprite.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            hideAllSelections();
            slots[1].selectionSprite.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            hideAllSelections();
            slots[2].selectionSprite.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            hideAllSelections();
            slots[3].selectionSprite.enabled = true;
        }
    }

    private  void hideAllSelections()
    {
        foreach (InventorySlot_UI slot in slots)
        {
            if(slot.selectionSprite != null)
            {
                slot.selectionSprite.enabled = false;
            }
        }
    }

    public override void AssignSlot(InventorySystem invToDisplay)
    {
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        if (slots.Length != inventorySystem.InventorySize) Debug.Log($"Inventory slots out of sync on {this.gameObject}");

        for (int i = 0; i < inventorySystem.InventorySize; i++)
        {
            slotDictionary.Add(slots[i], inventorySystem.InventorySlots[i]);
            slots[i].Init(inventorySystem.InventorySlots[i]);
        }
    }

}