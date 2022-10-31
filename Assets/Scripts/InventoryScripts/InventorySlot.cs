using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private InventoryItemData itemData;
    [SerializeField] private int stackSize;
    [SerializeField] private InventoryItemData.ITEMTYPES slotType;

    public InventoryItemData ItemData => itemData;
    public int StackSize => stackSize;
    public InventoryItemData.ITEMTYPES SlotType {
        get { return slotType; }
        set { this.slotType = value; }
    }

    public InventorySlot(InventoryItemData source, int amount, InventoryItemData.ITEMTYPES slotType)
    {
        itemData = source;
        stackSize = amount;
        this.slotType = slotType;
    }

    public InventorySlot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        itemData = null;
        stackSize = -1;
    }

    public void UpdateInventorySlot(InventoryItemData data, int amount)
    {
        itemData = data;
        stackSize = amount;
    }

    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = ItemData.MaxStackSize - stackSize;

        return RoomLeftInStack(amountToAdd);
    }

    public bool RoomLeftInStack(int amountToAdd)
    {
        if (stackSize + amountToAdd <= itemData.MaxStackSize) return true;
        else return false;
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
    }
}
