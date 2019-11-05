using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] private Transform itemsParent;
    [SerializeField] private KeyCode inventoryKey = KeyCode.E;
    [SerializeField] private InventorySlot[] slots;

    private void Start()
    {
        inventory.onItemsChanged += UpdateInventoryUI;
    }

    private void Update()
    {
        if (Input.GetKeyDown(inventoryKey))
        {
            inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
        }
    }

    public void UpdateInventoryUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.Items.Count)
            {
                slots[i].AddItemToSlot(inventory.Items[i]);
            }
            else
            {
                slots[i].RemoveItemInSlot();
            }
        }
    }
}
