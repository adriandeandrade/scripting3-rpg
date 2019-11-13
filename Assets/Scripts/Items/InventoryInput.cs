using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using enjoii.Items;
using enjoii.Characters;

public class InventoryInput : MonoBehaviour
{
    // Inspector Fields
    [Header("Inventory Input Configuration")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject equipmentPanel;
    [SerializeField] private GameObject dimBackground;
    [SerializeField] private List<KeyCode> toggleInventoryKeys = new List<KeyCode>();

    private bool isOpen = false;

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
                if (isOpen)
                    DisableInventoryUI();
                else
                    EnableInventoryUI();
            }
        }
    }

    public void EnableInventoryUI()
    {
        inventoryPanel.SetActive(true);
        equipmentPanel.SetActive(true);
        dimBackground.SetActive(true);

        isOpen = true;

        Time.timeScale = 0;
    }

    public void DisableInventoryUI()
    {
        inventoryPanel.SetActive(false);
        equipmentPanel.SetActive(false);
        dimBackground.SetActive(false);

        ItemContainer itemContainer;
        if (IsInsideItemContainer(out itemContainer))
        {
            ItemChest chestOpen = itemContainer as ItemChest;
            chestOpen.ToggleChestUI(false);
        }

        isOpen = false;

        Time.timeScale = 1;
    }

    private bool IsInsideItemContainer(out ItemContainer itemContainer)
    {
        Player player = GameManager.Instance.PlayerRef;
        itemContainer = player.ItemContainerOpen;

        if (itemContainer == null)
        {
            return false;
        }

        player.CloseItemContainer(itemContainer);
        return true;
    }
}
