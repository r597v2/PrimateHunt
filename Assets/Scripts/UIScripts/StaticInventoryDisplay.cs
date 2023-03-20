using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private InventorySlot_UI[] slots;
    [SerializeField] private int selectedSlot = 1;
    [SerializeField] GameObject playerGameObject;
    InventoryHolder inventory;
    InventoryItemData selectedInventoryItemData;

    public PlayerMovement playerMovementScript;

    public Image[] hotbarImages = new Image[] { };
    public TMP_Text[] hotbarItemCounts = new TMP_Text[] { };

    private HumidityMeter humidityMeterScript;

    void Awake()
    {
        HideAllSelections();
        slots[0].selectionSprite.enabled = true;

        foreach (Image image in hotbarImages)
        {
            image.CrossFadeAlpha(0f, 0.01f, false);
        }
        foreach (TMP_Text text in hotbarItemCounts)
        {
            text.CrossFadeAlpha(0f, 0.01f, false);
        }
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

        humidityMeterScript = playerGameObject.GetComponent<HumidityMeter>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            int previousSelectedSlot = selectedSlot;

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                HotbarOpacity();
                HideAllSelections();
                slots[0].selectionSprite.enabled = true;
                selectedSlot = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                HotbarOpacity();
                HideAllSelections();
                slots[1].selectionSprite.enabled = true;
                selectedSlot = 2;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                HotbarOpacity();
                HideAllSelections();
                slots[2].selectionSprite.enabled = true;
                selectedSlot = 3;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                HotbarOpacity();
                HideAllSelections();
                slots[3].selectionSprite.enabled = true;
                selectedSlot = 4;
            }

            if (inventory && selectedSlot != previousSelectedSlot)
            {
                selectedInventoryItemData = inventory.InventorySystem.GetFromInventorySlot(selectedSlot - 1);
                Debug.Log("Selected item: " + selectedInventoryItemData?.displayName);
            }

            if (selectedInventoryItemData == null)
            {
                if (playerMovementScript.currentAnimController != "default")
                {
                    playerMovementScript.SetDefaultAnimations();
                }

                if (playerMovementScript.currentAnimController == "default")
                {
                    if (Input.GetMouseButtonDown(1))
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
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        playerMovementScript.DropAk();
                        selectedInventoryItemData = null;
                    }
                }
            }
            else if (selectedInventoryItemData.displayName == "C4")
            {
                playerMovementScript.SetDefaultAnimations();
                playerMovementScript.SetC4Animations();

                if (playerMovementScript.currentAnimController == "c4")
                {
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        playerMovementScript.DropC4();
                        selectedInventoryItemData = null;
                    }

                    if (Input.GetMouseButton(0))
                    {
                        playerMovementScript.AimC4();
                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        playerMovementScript.ThrowC4();
                        selectedInventoryItemData = null;
                    }
                }
            }
            else if (selectedInventoryItemData.displayName == "Axe")
            {
                playerMovementScript.SetDefaultAnimations();
                playerMovementScript.SetAxeAnimations();

                if (playerMovementScript.currentAnimController == "axe")
                {
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        playerMovementScript.DropAxe();
                        selectedInventoryItemData = null;
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        playerMovementScript.AxeLightAttack();
                    }
                    if (Input.GetMouseButtonDown(1))
                    {
                        playerMovementScript.AxeHeavyAttack();
                    }
                }
            }
            else if (selectedInventoryItemData.displayName == "Humidity Meter")
            {
                playerMovementScript.SetDefaultAnimations();
                playerMovementScript.SetHumidityAnims();
                humidityMeterScript.EquipHumidityMeter();
            }

            foreach (Image image in hotbarImages)
            {
                image.CrossFadeAlpha(0f, 0.25f, false);
            }
            foreach (TMP_Text text in hotbarItemCounts)
            {
                text.CrossFadeAlpha(0f, 0.25f, false);
            }
        }
    }

    private void HotbarOpacity()
    {
        foreach (Image image in hotbarImages)
        {
            image.CrossFadeAlpha(127.5f, 0.01f, false);
        }
        foreach (TMP_Text text in hotbarItemCounts)
        {
            text.CrossFadeAlpha(255f, 0.01f, false);
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