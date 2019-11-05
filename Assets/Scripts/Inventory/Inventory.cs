using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private int maxInventorySize = 20;
    [SerializeField] private Item testItemToAdd;

    public List<Item> Items => items;

    public System.Action onItemsChanged;

    public void AddItem(Item itemToAdd)
    {
        if (items.Count >= maxInventorySize) return;

        items.Add(itemToAdd);
        onItemsChanged.Invoke();
    }

    public void RemoveItem(Item itemToRemove)
    {
        items.Remove(itemToRemove);
        onItemsChanged.Invoke();
    }

    [ContextMenu("Test Item")]
    public void AddTestItem()
    {
        AddItem(testItemToAdd);
    }
}
