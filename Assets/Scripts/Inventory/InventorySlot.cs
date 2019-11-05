using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image itemIconImage;

    private Item itemInSlot;
    private int itemQuantity;

    public int Quantity { get => itemQuantity; set => itemQuantity = value; }
    public Item ItemInSlot { get => itemInSlot; set => itemInSlot = value; }

    public void AddItemToSlot(Item newItem)
    {
        itemInSlot = newItem;

        itemIconImage.sprite = itemInSlot.itemIcon;
        itemIconImage.enabled = true;
    }

    public void RemoveItemInSlot()
    {
        itemInSlot = null;

        itemIconImage.sprite = null;
        itemIconImage.enabled = false;
    }
}
