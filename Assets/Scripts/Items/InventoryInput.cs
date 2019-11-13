using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using enjoii.Items;
using enjoii.Characters;

public class InventoryInput : MonoBehaviour
{
    // Inspector Fields
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject equipmentPanel;
    [SerializeField] private GameObject dimBackground;
    [SerializeField] private List<KeyCode> toggleInventoryKeys = new List<KeyCode>();


    private void Awake()
    {
        inventoryPanel.SetActive(false);
        equipmentPanel.SetActive(false);
        dimBackground.SetActive(false);
    }

    private void Update()
    {
        foreach (KeyCode key in toggleInventoryKeys)
        {
            if (Input.GetKeyDown(key))
            {
                ToggleInventoryUI();
            }
        }
    }

    public void ToggleInventoryUI()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        if (inventoryPanel.activeSelf)
        {
            equipmentPanel.SetActive(true);
            dimBackground.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            ItemContainer itemContainer;
            if(IsInsideItemContainer(out itemContainer))
            {
                ItemChest chestOpen = itemContainer as ItemChest;
                chestOpen.ToggleChestUI(false);
            }

            equipmentPanel.SetActive(false);
            dimBackground.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public bool IsInsideItemContainer(out ItemContainer itemContainer)
    {
        Player player = GameManager.Instance.PlayerRef;
        itemContainer = player.ItemContainerOpen;

        if (itemContainer == null)
        {
            return false;
        }

        return true;
    }
}
