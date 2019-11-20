﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using enjoii.Items;
using enjoii.Items.Slot;
using enjoii.Characters;

public class ItemSaveManager : MonoBehaviour
{
    // Inspector Fields

    // Private Variables
    private const string InventoryFileName = "Inventory";
    private const string EquipmentFileName = "Equipment";
    private const string CraftingFileName = "Crafting";

    private ItemDatabase itemDatabase;

    // Properties

    private void Start()
    {
        itemDatabase = FindObjectOfType<ItemDatabase>();
    }

    public void SaveInventory(SlotPanel slotPanel)
    {
        SaveItems(slotPanel.ItemSlots, InventoryFileName);
        Debug.Log($"Inventory saved");
    }

    public void SaveEquipment(SlotPanel slotPanel)
    {
        SaveItems(slotPanel.ItemSlots, EquipmentFileName);
        Debug.Log($"Equipment saved");
    }

    public void LoadInventory(SlotPanel slotPanel)
    {
        SlotPanelSaveData savedSlots = ItemSaveIO.LoadItems(InventoryFileName);

        if (savedSlots == null) return;

        GameManager.Instance.PlayerRef.Inventory.ClearItems();

        for(int i = 0; i < savedSlots.SavedSlots.Length; i++)
        {
            BaseItemSlot itemSlot = slotPanel.ItemSlots[i];
            BaseItemSlotData savedSlot = savedSlots.SavedSlots[i];

            if (savedSlot == null)
            {
                itemSlot.ItemInSlot = null;
            }
            else
            {
                itemSlot.ItemInSlot = itemDatabase.GetItem(savedSlot.itemID);
                itemSlot.UpdateSlot(itemSlot.ItemInSlot);
                GameManager.Instance.PlayerRef.Inventory.Items.Add(itemSlot.ItemInSlot);
            }
        }

        Debug.Log($"Inventory Loaded");
    }

    public void LoadEquipment(SlotPanel slotPanel)
    {
        SlotPanelSaveData savedSlots = ItemSaveIO.LoadItems(EquipmentFileName);

        if (savedSlots == null) return;

        GameManager.Instance.PlayerRef.EquipmentManager.UnequipAll();

        foreach(BaseItemSlotData savedSlot in savedSlots.SavedSlots)
        {
            if (savedSlot == null)
                continue;

            Item item = itemDatabase.GetItem(savedSlot.itemID);
            //GameManager.Instance.PlayerRef.Inventory.GiveItem(item.id);
            GameManager.Instance.PlayerRef.EquipmentManager.Equip(item as EquipmentItem);
        }

        Debug.Log($"Equipment Loaded");
    }

    private void SaveItems(IList<BaseItemSlot> itemSlots, string fileName)
    {
        var saveData = new SlotPanelSaveData(itemSlots.Count);

        for (int i = 0; i < saveData.SavedSlots.Length; i++)
        {
            BaseItemSlot itemSlot = itemSlots[i];

            if(itemSlot.ItemInSlot == null)
            {
                saveData.SavedSlots[i] = null;
            }
            else
            {
                saveData.SavedSlots[i] = new BaseItemSlotData(itemSlot.ItemInSlot.id);
            }
        }

        ItemSaveIO.SaveItems(saveData, fileName);
    }
}
