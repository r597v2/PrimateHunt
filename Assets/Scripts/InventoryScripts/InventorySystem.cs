using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> inventorySlots;

    public List<InventorySlot> InventorySlots => inventorySlots;
    public int InventorySize => InventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
    {
        if (ContainsItem(itemToAdd, out List<InventorySlot> invSlot)) // Check whether item exists in inventory
        {
            foreach (var slot in invSlot)
            {
                if (slot.RoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
        }

        if (HasFreeSlotForType(itemToAdd, out InventorySlot freeSlot)) // Gets the first available slot for the matching type
        {
            freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }

        return false;
    }

    public bool ContainsItem(InventoryItemData itemToCheck, out List<InventorySlot> invSlot)
    {
        invSlot = InventorySlots.Where(i => i.ItemData && i.ItemData.itemType == itemToCheck.itemType).ToList();
        Debug.Log(invSlot.Count);

        return invSlot == null ? false : true;
    }

    public bool HasFreeSlotForType(InventoryItemData itemToAdd, out InventorySlot freeSlot)
    {
        freeSlot = null;
        foreach(InventorySlot inventorySlot in InventorySlots)
        {
            if(inventorySlot.ItemData == null && inventorySlot.SlotType == itemToAdd.itemType)
            {
                freeSlot = inventorySlot;
                return true;
            }
        }

        return false;
    }

    // returns true if the item is retrieved.
    public bool RetrieveFromInventory(InventoryItemData itemToRetrieve, int amountToRetrieve) 
    {
        if(ContainsItem(itemToRetrieve, out List<InventorySlot> invSlot)) {
            foreach (var slot in invSlot)
            {
                if (slot.StackSize > 0) // available
                {
                    slot.RemoveFromStack(amountToRetrieve);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
        }

        return false;
    }

    // Get the item from inventory slot by its index.
    public InventoryItemData GetFromInventorySlot(int slotIndex) 
    {
        return inventorySlots[slotIndex].ItemData;
    }
}
