using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using enjoii.Items;


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
        private BaseItemSlot selectedSlot;

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
                UnEquip((EquippableItem)itemSlot.ItemInSlot);
            }
        }

        private void BeginDrag(BaseItemSlot itemSlot)
        {
            if (itemSlot.ItemInSlot != null)
            {
                selectedSlot = itemSlot;
                handSlotImage.sprite = itemSlot.ItemInSlot.ItemIcon;
                handSlotImage.transform.position = Input.mousePosition;
                handSlotImage.gameObject.SetActive(true);
            }
        }

        private void Drag(BaseItemSlot itemSlot)
        {
            if(handSlotImage.enabled)
            {
                handSlotImage.transform.position = Input.mousePosition;
            }
        }

        private void EndDrag(BaseItemSlot itemSlot)
        {
            selectedSlot = null;
            handSlotImage.gameObject.SetActive(false);
        }

        private void Drop(BaseItemSlot dropItemSlot)
        {
            if (dropItemSlot is EquipmentSlot) return;
            if (selectedSlot == null) return;

            if(dropItemSlot != null)
            {
                if (dropItemSlot.CanAddStack(selectedSlot.ItemInSlot))
                {
                    AddStacks(dropItemSlot);
                }
                else if (dropItemSlot.CanRecieveItem(selectedSlot.ItemInSlot) && selectedSlot.CanRecieveItem(dropItemSlot.ItemInSlot))
                {
                    SwapItems(dropItemSlot);
                }
            }
        }

        private void AddStacks(BaseItemSlot dropItemSlot)
        {
            int spaceLeftInSlot = dropItemSlot.ItemInSlot.MaxStackSize - dropItemSlot.ItemQuantity;
            int amountToAdd = Mathf.Min(spaceLeftInSlot, selectedSlot.ItemQuantity);

            dropItemSlot.ItemQuantity += amountToAdd;
            selectedSlot.ItemQuantity -= amountToAdd;
        }

        private void SwapItems(BaseItemSlot dropItemSlot)
        {
            Item selectedSlotItem = selectedSlot.ItemInSlot;
            int selectedSlotItemQuantity = selectedSlot.ItemQuantity;

            selectedSlot.ItemInSlot = dropItemSlot.ItemInSlot;
            selectedSlot.ItemQuantity = dropItemSlot.ItemQuantity;

            dropItemSlot.ItemInSlot = selectedSlotItem;
            dropItemSlot.ItemQuantity = selectedSlotItemQuantity;
        }

        private void DestroyItem()
        {
            if (selectedSlot == null) return;

            BaseItemSlot slot = selectedSlot;
            DestroyItemInSlot(slot);
        }

        private void DestroyItemInSlot(BaseItemSlot itemSlot)
        {
            if (itemSlot is EquipmentSlot)
            {
                UnEquip((EquippableItem)itemSlot.ItemInSlot);
            }

            itemSlot.ItemInSlot.Destroy();
            itemSlot.ItemInSlot = null;
        }

        public void Equip(EquippableItem item)
        {
            if (inventory.RemoveItem(item))
            {
                EquippableItem previousItem;
                if (equipmentPanel.AddItem(item, out previousItem))
                {
                    if (previousItem != null) // Check to see if we already have something equipped.
                    {
                        inventory.AddItem(previousItem);
                    }

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
            if (inventory.CanAddItem(item) && equipmentPanel.RemoveItem(item))
            {
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
