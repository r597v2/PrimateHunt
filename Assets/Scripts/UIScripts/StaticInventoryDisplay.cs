using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private InventorySlot_UI[] slots;
    [SerializeField] private int selectedSlot = 1;
    [SerializeField] GameObject playerGameObject;
    InventoryHolder inventory;
    InventoryItemData selectedInventoryItemData;

    public PlayerMovement playerMovementScript;

    void Awake()
    {
        HideAllSelections();
        slots[0].selectionSprite.enabled = true;
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

        inventory = playerGameObject?.transform.GetComponent<InventoryHolder>();
        selectedInventoryItemData = inventory?.InventorySystem.GetFromInventorySlot(selectedSlot - 1);
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            int previousSelectedSlot = selectedSlot;

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                HideAllSelections();
                slots[0].selectionSprite.enabled = true;
                selectedSlot = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                HideAllSelections();
                slots[1].selectionSprite.enabled = true;
                selectedSlot = 2;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                HideAllSelections();
                slots[2].selectionSprite.enabled = true;
                selectedSlot = 3;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                HideAllSelections();
                slots[3].selectionSprite.enabled = true;
                selectedSlot = 4;
            }

            if (inventory && selectedSlot != previousSelectedSlot)
            {
                selectedInventoryItemData = inventory.InventorySystem.GetFromInventorySlot(selectedSlot - 1);
                Debug.Log("Selected item: " + selectedInventoryItemData?.description);
            }

            if (selectedInventoryItemData == null)
            {
                if (playerMovementScript.currentAnimController != "default")
                {
                    playerMovementScript.SetDefaultAnimations();
                }

                if (playerMovementScript.currentAnimController == "default")
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        playerMovementScript.Headbutt();
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        playerMovementScript.Slap();
                    }
                }
            }
            else if (selectedInventoryItemData.displayName == "AK47")
            {
                playerMovementScript.SetAKAnimations();

                if (playerMovementScript.currentAnimController == "ak")
                {
                    if (Input.GetKeyDown(KeyCode.G))
                    {
                        playerMovementScript.DropAk();
                        selectedInventoryItemData = null;
                    }
                }
            }
        }
    }

    private void HideAllSelections()
    {
        foreach (InventorySlot_UI slot in slots)
        {
            if (slot.selectionSprite != null)
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