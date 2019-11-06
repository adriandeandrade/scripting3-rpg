using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> slots = new List<InventorySlot>();
    [SerializeField] [Range(5, 20)] private int inventorySize = 5;
    [SerializeField] private GameObject inventorySlotPrefab;
    [SerializeField] private Transform slotsParent;
    [SerializeField] private Item testItem = null;
    [SerializeField] private int testIndex;

    public System.Action onItemsChanged;

    private void Awake()
    {
        InitializeSlots();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            AddItemTest();
        }
    }

    public void InitializeSlots()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            InventorySlot slot = Instantiate(inventorySlotPrefab, slotsParent).GetComponent<InventorySlot>();
            slots.Add(slot);
            slot.SlotIndex = i;
        }
    }

    public void AddItem(Item item)
    {
        if (item == null) return;

        if(item.MaxStackSize > 0) // If item can be stacked essentially.
        {
            if(PlaceInStack(item)) // Try and place the item in an existing stack.
            {
                return;
            }
        }

        PlaceInEmptySlot(item);
    }

    private void PlaceInEmptySlot(Item item)
    {
        foreach (InventorySlot slot in slots)
        {
            if(slot.IsEmpty)
            {
                if (slot.AddItemToSlot(item))
                {
                    Debug.Log($"{item.ItemName} was successfully added to a slot.");
                    return;
                }
            }
        }
    }

    private bool PlaceInStack(Item item)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.StackItem(item))
            {
                Debug.Log($"{item.ItemName} was successfully stacked.");
                return true;
            }
        }

        return false;
    }

    public void Swap(int indexOne, int indexTwo)
    {
        InventorySlot fromSlot = slots[indexOne];
        InventorySlot toSlot = slots[indexTwo];
        Debug.Log($"Swapping from {fromSlot.SlotIndex} to {toSlot.SlotIndex}");

        if (fromSlot.Equals(toSlot)) return;

        if(!toSlot.IsEmpty)
        {
            if(fromSlot.ItemInSlot == toSlot.ItemInSlot)
            {
                if(PlaceInStack(fromSlot.ItemInSlot))
                {
                    fromSlot.RemoveItemInSlot();
                    Debug.Log($"");
                    return;
                }
            }
        }

        slots[indexOne] = toSlot;
        slots[indexTwo] = fromSlot;
        Debug.Log($"{slots[indexOne].Quantity}");
        Debug.Log($"{slots[indexTwo].Quantity}");
    }

    public void RemoveItem(Item item)
    {
        foreach (InventorySlot slot in slots)
        {

        }
    }

    public void RemoveItemAtIndex(int index)
    {
        if (index > slots.Count || index < 0) return;
        if (slots[index].IsEmpty) return;

        slots[index].RemoveItemInSlot();
    }

    [ContextMenu("Test Item")]
    public void AddItemTest()
    {
        AddItem(testItem);
    }

    [ContextMenu("Remove Test")]
    public void RemoveAtIndexTest()
    {
        RemoveItemAtIndex(testIndex);
    }

    public InventorySlot GetSlotAtIndex(int index)
    {
        return slots[index];
    }
}
