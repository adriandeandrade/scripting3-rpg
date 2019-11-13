using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using enjoii.Items;
using enjoii.Stats;


namespace enjoii.Characters
{
    public class Player : BaseCharacter
    {
        // Inspector Fields
        [Header("Character Configuration")]
        [SerializeField] private NotificationManager notificationManager;
        [SerializeField] private WeaponController weaponController;
        [SerializeField] private Transform weaponSlot;
        [SerializeField] private GameObject bowPrefab;

        [Header("Stats")]
        [SerializeField] private PlayerStats playerStats;
        [SerializeField] private CharacterStats characterStats;

        // Properties
        public CharacterStats CharacterStats => characterStats;

        private void OnValidate()
        {
            if (itemToolTip == null)
            {
                itemToolTip = FindObjectOfType<ItemToolTip>();
            }

            if (playerStats == null)
            {
                playerStats = GetComponent<PlayerStats>();
            }

            if (inventoryInput == null)
            {
                inventoryInput = GetComponent<InventoryInput>();
            }

            if(weaponController == null)
            {
                weaponController = GetComponent<WeaponController>();
            }
        }

        protected override void Awake()
        {
            SubscribeEvents();
            characterStats = new CharacterStats(5, 5);
            base.Awake();
        }

        #region Inventory Variables
        [Header("Player Inventory Configuration")]
        [SerializeField] private Inventory inventory;
        [SerializeField] private InventoryInput inventoryInput;

        [SerializeField] private ItemToolTip itemToolTip;
        [SerializeField] private Image handSlotImage;

        [Header("Panels")]
        [SerializeField] private EquipmentPanel equipmentPanel;
        [SerializeField] private DestroyItemSlot destroyItemSlot;

        // Private Variables
        private BaseItemSlot handSlot;

        // Properties
        public Inventory Inventory => inventory;
        public EquipmentPanel EquipmentContainer => equipmentPanel;
        public ItemContainer ItemContainerOpen => openItemContainer;

        // Components
        private ItemContainer openItemContainer;

        // Events
        public event Action<EquippableItem> OnWeaponEquipped;
        public event Action OnWeaponDequipped;
        #endregion

        #region Inventory Functions
        private void SubscribeEvents()
        {
            inventory.OnRightClickEvent += InventoryRightClick;
            inventory.OnBeginDragEvent += BeginDrag;
            inventory.OnEndDragEvent += EndDrag;
            inventory.OnDragEvent += Drag;
            inventory.OnDropEvent += Drop;
            inventory.OnPointerEnterEvent += ShowToolTip;
            inventory.OnPointerExitEvent += HideToolTip;

            equipmentPanel.OnRightClickEvent += EquipmentPanelRightClick;
            equipmentPanel.OnPointerEnterEvent += ShowToolTip;
            equipmentPanel.OnPointerExitEvent += HideToolTip;
            equipmentPanel.OnBeginDragEvent += BeginDrag;
            equipmentPanel.OnEndDragEvent += EndDrag;
            equipmentPanel.OnDragEvent += Drag;
            equipmentPanel.OnDropEvent += Drop;

            destroyItemSlot.OnDropEvent += DestroyItem;
        }

        private void InventoryRightClick(BaseItemSlot itemSlot)
        {
            if (itemSlot.ItemInSlot is EquippableItem)
            {
                EquippableItem item = itemSlot.ItemInSlot as EquippableItem;
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

        private void EquipmentPanelRightClick(BaseItemSlot itemSlot)
        {
            if (itemSlot.ItemInSlot is EquippableItem)
            {
                EquippableItem item = itemSlot.ItemInSlot as EquippableItem;
                UnEquip((EquippableItem)itemSlot.ItemInSlot);
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

            if (dropItemSlot.ItemInSlot is EquippableItem)
            {
                Equip((EquippableItem)dropItemSlot.ItemInSlot);
                return;
            }

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

            //if (dropItemSlot is EquipmentSlot)
            //{
            //    if (equippableItemInHand != null) equippableItemInHand.Equip(this);
            //    if (equippableItemInDropSlot != null) equippableItemInDropSlot.UnEquip(this);
            //}

            //if (handSlot is EquipmentSlot)
            //{
            //    if (equippableItemInHand != null) equippableItemInHand.UnEquip(this);
            //    if (equippableItemInDropSlot != null) equippableItemInDropSlot.Equip(this);
            //}

            if (dropItemSlot is EquipmentSlot)
            {
                if (equippableItemInHand != null) Equip(equippableItemInHand);
                if (equippableItemInDropSlot != null) UnEquip(equippableItemInDropSlot);
            }

            if (handSlot is EquipmentSlot)
            {
                if (equippableItemInHand != null) UnEquip(equippableItemInHand);
                if (equippableItemInDropSlot != null) Equip(equippableItemInDropSlot);
            }

            Item itemInHand = handSlot.ItemInSlot;
            int itemInHandQuantity = handSlot.ItemQuantity;

            handSlot.ItemInSlot = dropItemSlot.ItemInSlot;
            handSlot.ItemQuantity = dropItemSlot.ItemQuantity;

            dropItemSlot.ItemInSlot = itemInHand;
            dropItemSlot.ItemQuantity = itemInHandQuantity;
        }

        private void DestroyItem()
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
            if (inventory.RemoveItem(item))
            {
                EquippableItem previousItem;
                if (equipmentPanel.EquipItem(item, out previousItem))
                {
                    if (previousItem != null) // Check to see if we already have something equipped.
                    {
                        //inventory.AddItem(previousItem);
                        UnEquip(previousItem);
                        //previousItem.UnEquip(this);
                        
                    }

                    //item.Equip(this);
                    OnWeaponEquipped(item);
                }
                else
                {
                    inventory.AddItem(item);
                }
            }
        }

        public void UnEquip(EquippableItem item)
        {
            if (inventory.CanAddItem(item) && equipmentPanel.UnEquipItem(item))
            {
                //item.UnEquip(this);
                inventory.AddItem(item);
                OnWeaponDequipped();
            }
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
            inventoryInput.EnableInventoryUI();

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
            inventoryInput.DisableInventoryUI();

            inventory.OnRightClickEvent += InventoryRightClick;
            inventory.OnRightClickEvent -= TransferToItemContainer;

            otherItemContainer.OnBeginDragEvent -= BeginDrag;
            otherItemContainer.OnEndDragEvent -= EndDrag;
            otherItemContainer.OnDragEvent -= Drag;
            otherItemContainer.OnDropEvent -= Drop;
        }

        private void ShowToolTip(BaseItemSlot itemSlot)
        {
            if (itemSlot.ItemInSlot != null)
            {
                itemToolTip.ShowToolTip(itemSlot.ItemInSlot);
            }
        }

        private void HideToolTip(BaseItemSlot itemSlot)
        {
            if (itemToolTip.gameObject.activeSelf)
            {
                itemToolTip.HideToolTip();
            }
        }
        #endregion

        public void OnXPAdded(float amount)
        {
            playerStats.AddXP(amount);
            Debug.Log($"{amount} of XP was gained.");
        }

        public bool PickupItem(Item item)
        {
            Item itemCopy = item.GetCopy();
            if (inventory.AddItem(itemCopy))
            {
                notificationManager.SpawnNotification(item);
                return true;
            }
            else
            {
                itemCopy.Destroy();
                return false;
            }
        }
    }
}
