using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    [Header("Item Data")]
    [SerializeField] private string itemName = "New Item Name";
    [SerializeField] private string itemDescription = "New Item Description";
    [SerializeField] private int maxStackSize = 1;
    [SerializeField] private Sprite itemIcon = null;
    private InventorySlot itemSlot;

    public string ItemName { get => itemName; }
    public string ItemDescription { get => itemDescription; }
    public int MaxStackSize { get => maxStackSize; }
    public Sprite ItemIcon { get => itemIcon; }
    public InventorySlot ItemSlot { get => itemSlot; set => itemSlot = value; }
}
