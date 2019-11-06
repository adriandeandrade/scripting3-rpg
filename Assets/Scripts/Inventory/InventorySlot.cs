using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Image itemIconImage = null;
    [SerializeField] private TextMeshProUGUI itemQuantityText = null;

    private Stack<Item> items = new Stack<Item>();
    private int slotIndex = -1;

    public int Quantity { get => items.Count; }
    public bool IsEmpty { get => items.Count == 0; }
    public int SlotIndex { get => slotIndex; set => slotIndex = value; }
    public Item ItemInSlot
    {
        get
        {
            if (!IsEmpty)
            {
                return items.Peek();
            }

            return null;
        }
    }

    private void Awake()
    {
        inventory = GetComponentInParent<Inventory>();
        if (IsEmpty) itemQuantityText.SetText("");
    }

    public bool AddItemToSlot(Item newItem)
    {
        items.Push(newItem);

        itemIconImage.sprite = newItem.ItemIcon;
        itemIconImage.enabled = true;
        newItem.ItemSlot = this;
        UpdateSlotUI();

        return true;
    }

    public void RemoveItemInSlot()
    {
        itemIconImage.sprite = null;
        itemIconImage.enabled = false;
        UpdateSlotUI();
    }

    public bool StackItem(Item item)
    {
        if (!IsEmpty && item == ItemInSlot && Quantity < ItemInSlot.MaxStackSize)
        {
            AddItemToSlot(item);
            UpdateSlotUI();
            return true;
        }

        return false;
    }

    public void UpdateSlotUI()
    {
        itemQuantityText.SetText(Quantity > 0 ? Quantity.ToString() : "");
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

        if (itemDragHandler == null)
        {
            Debug.Log("Drag handler not found.");
            return;
        }

        InventorySlot inventorySlot = eventData.pointerEnter.GetComponent<InventorySlot>();

        if(inventorySlot != null)
        {
            inventory.Swap(itemDragHandler.InventorySlot.SlotIndex, slotIndex);
        }
    }
}
