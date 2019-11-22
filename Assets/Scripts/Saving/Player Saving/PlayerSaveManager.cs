using enjoii.Items;
using enjoii.Items.Slot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveManager : MonoBehaviour
{
    public SlotPanelData SavePanel(SlotPanel panel)
    {
        var saveData = new SlotPanelData(panel.ItemSlots.Count);

        for (int i = 0; i < saveData.SavedSlots.Length; i++)
        {
            BaseItemSlot itemSlot = panel.ItemSlots[i];

            if (itemSlot.ItemInSlot == null)
            {
                saveData.SavedSlots[i] = null;
            }
            else
            {
                saveData.SavedSlots[i] = new BaseItemSlotData(itemSlot.ItemInSlot.id);
            }
        }

        return saveData;
    }

    public void LoadInventoryPanel(Inventory inventory, SlotPanel panel, SlotPanelData panelData)
    {
        if (panelData == null) return;

        panel.EmptyAllSlots();
        inventory.ClearItems();

        for (int i = 0; i < panelData.SavedSlots.Length; i++)
        {
            BaseItemSlot itemSlot = panel.ItemSlots[i];
            BaseItemSlotData savedSlot = panelData.SavedSlots[i];

            if (savedSlot.itemID == 0)
            {
                itemSlot.ItemInSlot = null;
            }
            else
            {
                itemSlot.ItemInSlot = GameManager.Instance.ItemDatabase.GetItem(savedSlot.itemID);
                itemSlot.UpdateSlot(itemSlot.ItemInSlot);
                inventory.Items.Add(itemSlot.ItemInSlot);
            }
        }
    }

    public void LoadEquipmentPanel(SlotPanel panel, SlotPanelData panelData)
    {
        if (panelData == null) return;

        panel.EmptyAllSlots();
        GameManager.Instance.PlayerRef.EquipmentManager.UnequipAll();

        foreach (BaseItemSlotData savedSlot in panelData.SavedSlots)
        {
            if (savedSlot == null)
                continue;

            Item item = GameManager.Instance.ItemDatabase.GetItem(savedSlot.itemID);
            GameManager.Instance.PlayerRef.EquipmentManager.Equip(item as EquipmentItem);
        }

        //Debug.Log($"Equipment Loaded");
    }

    public PlayerStatData SavePlayerStats(PlayerStats _playerStats)
    {
        var saveData = new PlayerStatData(_playerStats.CurrentLevel, _playerStats.CurrentXP);

        saveData.currentLevel = _playerStats.CurrentLevel;
        saveData.currentXP = _playerStats.CurrentXP;

        return saveData;
    }

    public void LoadPlayerStats(PlayerStats _playerStats, PlayerStatData playerStatsData)
    {
        _playerStats.Load(playerStatsData);
    }

    public TransformData SavePlayerTransform(Transform _transform)
    {
        var saveData = new TransformData(_transform);
        return saveData;
    }

    public void LoadPlayerTransform(Transform _transform, TransformData tranformData)
    {
        _transform.localPosition = new Vector3(tranformData._position[0], tranformData._position[1], tranformData._position[2]);
        _transform.localRotation = new Quaternion(tranformData._rotation[1], tranformData._rotation[2], tranformData._rotation[3], tranformData._rotation[0]);
        _transform.localScale = new Vector3(tranformData._scale[0], tranformData._scale[1], tranformData._scale[2]);
    }
}
