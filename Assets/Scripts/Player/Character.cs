using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using enjoii.Items;


namespace enjoii.Characters
{
    public class Character : MonoBehaviour
    {
        // Inspector Fields
        [Header("Character Configuration")]
        [SerializeField] private int health = 50;
        [SerializeField] private Inventory inventory;
        [SerializeField] private Image handSlotImage;

        // Private Variables
        private BaseItemSlot handSlot;

        // Properties
        public Inventory Inventory => inventory;

        // Components
        private ItemContainer openItemContainer;

        // Events

        private void OnValidate()
        {

        }

        private void Awake()
        {
            inventory.OnRightClickEvent += InventoryRightClick;
            inventory.OnBeginDragEvent += BeginDrag;
            inventory.OnEndDragEvent += EndDrag;
            inventory.OnDragEvent += Drag;
            inventory.OnDropEvent += Drop;
            //inventory.OnPointerEnterEvent += ShowToolTip;
            //inventory.OnPointerExitEvent += HideToolTip;

        }

        private void InventoryRightClick(BaseItemSlot itemSlot)
        {
            if (itemSlot.ItemInSlot is EquippableItem)
            {
                Equip((EquippableItem)itemSlot.ItemInSlot);
            }
            else if (itemSlot.ItemInSlot is UsableItem)
            {
                UsableItem usableItem = (UsableItem)itemSlot.ItemInSlot;
                usableItem.Use(this);
                if (usableItem.IsConsumable)
                {
                    itemSlot.ItemQuantity--;
                    usableItem.Destroy();
                }
            }

        }

        private void BeginDrag(BaseItemSlot itemSlot)
        {
            if (itemSlot.ItemInSlot != null)
            {
                handSlot = itemSlot;
                handSlotImage.sprite = itemSlot.ItemInSlot.ItemIcon;
                handSlotImage.transform.position = Input.mousePosition;
                handSlotImage.gameObject.SetActive(true);
            }
        }

        private void Drag(BaseItemSlot itemSlot)
        {
            handSlotImage.transform.position = Input.mousePosition;
        }

        private void EndDrag(BaseItemSlot itemSlot)
        {
            handSlot = null;
            handSlotImage.gameObject.SetActive(false);
        }

        private void Drop(BaseItemSlot dropItemSlot)
        {
            if (handSlot == null) return;

            if (dropItemSlot.CanAddStack(handSlot.ItemInSlot))
            {
                AddStacks(dropItemSlot);
            }
            else if (dropItemSlot.CanRecieveItem(handSlot.ItemInSlot) && handSlot.CanRecieveItem(dropItemSlot.ItemInSlot))
            {
                SwapItems(dropItemSlot);
            }
        }

        private void AddStacks(BaseItemSlot dropItemSlot)
        {
            int spaceLeftInSlot = dropItemSlot.ItemInSlot.MaxStackSize - dropItemSlot.ItemQuantity;
            int amountToAdd = Mathf.Min(spaceLeftInSlot, handSlot.ItemQuantity);

            dropItemSlot.ItemQuantity += amountToAdd;
            handSlot.ItemQuantity -= amountToAdd;
        }

        private void SwapItems(BaseItemSlot dropItemSlot)
        {
            EquippableItem equippableItemInHand = handSlot.ItemInSlot as EquippableItem;
            EquippableItem equippableItemInDropSlot = dropItemSlot.ItemInSlot as EquippableItem;

            if (dropItemSlot is EquipmentSlot)
            {
                if (equippableItemInHand != null) equippableItemInHand.Equip(this);
                if (equippableItemInDropSlot != null) equippableItemInDropSlot.UnEquip(this);
            }

            if (handSlot is EquipmentSlot)
            {
                if (equippableItemInHand != null) equippableItemInHand.UnEquip(this);
                if (equippableItemInDropSlot != null) equippableItemInDropSlot.Equip(this);
            }

            Item itemInHand = handSlot.ItemInSlot;
            int itemInHandQuantity = handSlot.ItemQuantity;

            handSlot.ItemInSlot = dropItemSlot.ItemInSlot;
            handSlot.ItemQuantity = dropItemSlot.ItemQuantity;

            dropItemSlot.ItemInSlot = itemInHand;
            dropItemSlot.ItemQuantity = itemInHandQuantity;

        }

        private void DropItemOutsideUI()
        {
            if (handSlot == null) return;
            BaseItemSlot slot = handSlot;
            DestroyItemInSlot(slot);
        }

        private void DestroyItemInSlot(BaseItemSlot itemSlot)
        {
            if (itemSlot is EquipmentSlot)
            {
                EquippableItem equippableItem = (EquippableItem)itemSlot.ItemInSlot;
                equippableItem.UnEquip(this);
            }

            itemSlot.ItemInSlot.Destroy();
            itemSlot.ItemInSlot = null;
        }

        public void Equip(EquippableItem item)
        {
            Debug.Log($"Character successfully equipped {item}");
        }

        public void UnEquip(EquippableItem item)
        {
            Debug.Log($"Character successfully un-equipped {item}");

        }

        private void TransferToItemContainer(BaseItemSlot itemSlot)
        {
            Item item = itemSlot.ItemInSlot;

            if (item != null && openItemContainer.CanAddItem(item))
            {
                inventory.RemoveItem(item);
                openItemContainer.AddItem(item);
            }
        }

        private void TransferToInventory(BaseItemSlot itemSlot)
        {
            Item item = itemSlot.ItemInSlot;

            if (item != null && inventory.CanAddItem(item))
            {
                openItemContainer.RemoveItem(item);
                inventory.AddItem(item);
            }
        }

        public void OpenItemContainer(ItemContainer otherItemContainer)
        {
            openItemContainer = otherItemContainer;
            inventory.OnRightClickEvent -= InventoryRightClick;
            inventory.OnRightClickEvent += TransferToItemContainer;

            otherItemContainer.OnRightClickEvent += TransferToInventory;

            otherItemContainer.OnBeginDragEvent += BeginDrag;
            otherItemContainer.OnEndDragEvent += EndDrag;
            otherItemContainer.OnDragEvent += Drag;
            otherItemContainer.OnDropEvent += Drop;
        }

        public void CloseItemContainer(ItemContainer otherItemContainer)
        {
            openItemContainer = null;

            inventory.OnRightClickEvent += InventoryRightClick;
            inventory.OnRightClickEvent -= TransferToItemContainer;

            otherItemContainer.OnBeginDragEvent -= BeginDrag;
            otherItemContainer.OnEndDragEvent -= EndDrag;
            otherItemContainer.OnDragEvent -= Drag;
            otherItemContainer.OnDropEvent -= Drop;
        }
    }
}
