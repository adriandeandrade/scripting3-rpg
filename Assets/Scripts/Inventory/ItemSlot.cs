using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace enjoii.Items
{
    public enum SlotType
    {
        ItemSlot,
        CraftingSlot,
        CraftingResultSlot,
        EquippableSlot,
        TrashSlot
    }

    public class ItemSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        // Inspector Fields
        [SerializeField] private Image itemIconImage;
        [SerializeField] private Image slotImage;
        [SerializeField] private SlotType slotType;
        [SerializeField] private bool craftingResultSlot = false;

        [Header("Slot Configuration")]
        [SerializeField] private Sprite trashSlotSprite;
        [SerializeField] private Sprite equippableSlotSprite;

        // Private Variables
        private Item itemInSlot;
        private ItemSlot selectedItem;

        private CraftingPanel craftingSlots;
        private EquipmentPanel equippableSlots;
        private Tooltip tooltip;
        private Inventory inventory;

        // Properties
        public Item ItemInSlot { get => itemInSlot; set => itemInSlot = value; }
        public SlotType SlotType { get => slotType; set => slotType = value; }
        

        private void Awake()
        {
            craftingSlots = FindObjectOfType<CraftingPanel>();

            tooltip = FindObjectOfType<Tooltip>();
            selectedItem = GameObject.Find("HandSlot").GetComponent<ItemSlot>();
        }

        private void Start()
        {
            inventory = GameManager.Instance.PlayerRef.Inventory;
            InitializeSlot();
            ClearSlot();
        }

        private void InitializeSlot()
        {
            switch(slotType)
            {
                case SlotType.CraftingResultSlot:
                    break;

                case SlotType.CraftingSlot:
                    break;

                case SlotType.EquippableSlot:
                    break;

                case SlotType.ItemSlot:
                    break;

                case SlotType.TrashSlot:
                    slotImage.sprite = trashSlotSprite;
                    break;
            }
        }

        public void UpdateSlot(Item item)
        {
            this.itemInSlot = item;

            if (this.itemInSlot != null)
            {
                itemIconImage.color = Color.white;
                itemIconImage.sprite = item.icon;
            }
            else
            {
                itemIconImage.color = Color.clear;
            }

            if (slotType == SlotType.CraftingSlot)
            {
                craftingSlots.UpdateRecipe();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            switch(slotType)
            {
                case SlotType.CraftingResultSlot:
                    HandleCraftingResultSlot();
                    break;

                case SlotType.CraftingSlot:
                    HandleCraftingSlot(eventData);
                    break;

                case SlotType.EquippableSlot:
                    HandleEquipmentSlot(eventData);
                    break;

                case SlotType.ItemSlot:
                    HandleItemSlot(eventData);
                    break;

                case SlotType.TrashSlot:
                    HandleTrashSlot(eventData);
                    break;
            }
        }

        private void HandleEquipmentSlot(PointerEventData eventData)
        {
            if (this.itemInSlot == null) return;
            
            if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
            {
                EquipmentItem equippable = this.itemInSlot as EquipmentItem;

                int slotIndex = GameManager.Instance.PlayerRef.EquipmentManager.GetSlotIndex((equippable));

                Debug.Log($"{this.itemInSlot.name} has been unequipped.");
                GameManager.Instance.PlayerRef.EquipmentManager.Unequip(slotIndex);

                ClearSlot();
            }
        }

        private void HandleItemSlot(PointerEventData eventData)
        {
            if (this.itemInSlot != null)
            {
                if(this.itemInSlot is EquipmentItem)
                {
                    if(eventData != null && eventData.button == PointerEventData.InputButton.Right)
                    {
                        EquipmentItem clone = new EquipmentItem(this.itemInSlot as EquipmentItem);
                        GameManager.Instance.PlayerRef.EquipmentManager.Equip(clone);
                        Debug.Log($"{this.itemInSlot.name} has been equipped.");
                        ClearSlot();

                        return;
                    }
                }

                if (selectedItem.itemInSlot != null) // If we are dragging an item and there's an item in the slot.
                {
                    Item clone = new Item(selectedItem.itemInSlot);
                    selectedItem.UpdateSlot(this.itemInSlot);
                    UpdateSlot(clone);
                }
                else
                {
                    selectedItem.UpdateSlot(itemInSlot);
                    ClearSlot();
                }
            }
            else if (selectedItem.itemInSlot != null && !craftingResultSlot && slotType != SlotType.EquippableSlot)
            {
                UpdateSlot(selectedItem.itemInSlot);
                selectedItem.ClearSlot();
            }
        }

        private void HandleCraftingSlot(PointerEventData eventData)
        {
            HandleItemSlot(eventData);
            craftingSlots.UpdateRecipe();
        }

        private void HandleCraftingResultSlot()
        {
            UICraftResult craftResult = GetComponent<UICraftResult>();

            if (craftResult != null && this.itemInSlot != null && selectedItem.itemInSlot == null)
            {
                craftResult.PickItem();
                selectedItem.UpdateSlot(this.itemInSlot);
                craftResult.ClearSlots();
            }
        }

        private void HandleTrashSlot(PointerEventData eventData)
        {
            if(selectedItem.ItemInSlot != null)
            {
                GameManager.Instance.PlayerRef.Inventory.RemoveItem(selectedItem.ItemInSlot.id);
                selectedItem.ClearSlot();
                ClearSlot();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (this.itemInSlot != null)
            {
                tooltip.GenerateToolTip(this.itemInSlot);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tooltip.gameObject.SetActive(false);
        }

        private void ClearSlot()
        {
            this.itemInSlot = null;
            itemIconImage.color = Color.clear;
        }
    }
}