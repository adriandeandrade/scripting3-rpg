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
        EquippableSlot
    }


    public class ItemSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Item item;
        public SlotType slotType;
        public bool craftingResultSlot = false;

        [SerializeField] private Image itemIconImage;
        private ItemSlot selectedItem;
        private CraftingPanel craftingSlots;
        private EquipmentPanel equippableSlots;
        private Tooltip tooltip;
        private Inventory inventory;

        private void Awake()
        {
            craftingSlots = FindObjectOfType<CraftingPanel>();

            tooltip = FindObjectOfType<Tooltip>();
            selectedItem = GameObject.Find("HandSlot").GetComponent<ItemSlot>();
        }

        private void Start()
        {
            inventory = GameManager.Instance.PlayerRef.Inventory;
            ClearSlot();
        }

        public void UpdateSlot(Item item)
        {
            this.item = item;

            if (this.item != null)
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
            }
        }

        private void HandleEquipmentSlot(PointerEventData eventData)
        {
            if (this.item == null) return;
            
            if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
            {
                EquipmentItem equippable = this.item as EquipmentItem;

                int slotIndex = GameManager.Instance.PlayerRef.EquipmentManager.GetSlotIndex((equippable));

                Debug.Log($"{this.item.name} has been unequipped.");
                GameManager.Instance.PlayerRef.EquipmentManager.Unequip(slotIndex);

                ClearSlot();
            }
        }

        private void HandleItemSlot(PointerEventData eventData)
        {
            if (this.item != null)
            {
                if(this.item is EquipmentItem)
                {
                    if(eventData != null && eventData.button == PointerEventData.InputButton.Right)
                    {
                        EquipmentItem clone = new EquipmentItem(this.item as EquipmentItem);
                        GameManager.Instance.PlayerRef.EquipmentManager.Equip(clone);
                        Debug.Log($"{this.item.name} has been equipped.");
                        ClearSlot();

                        return;
                    }
                }

                if (selectedItem.item != null) // If we are dragging an item and there's an item in the slot.
                {
                    Item clone = new Item(selectedItem.item);
                    selectedItem.UpdateSlot(this.item);
                    UpdateSlot(clone);
                }
                else
                {
                    selectedItem.UpdateSlot(item);
                    ClearSlot();
                }
            }
            else if (selectedItem.item != null && !craftingResultSlot && slotType != SlotType.EquippableSlot)
            {
                UpdateSlot(selectedItem.item);
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

            if (craftResult != null && this.item != null && selectedItem.item == null)
            {
                craftResult.PickItem();
                selectedItem.UpdateSlot(this.item);
                craftResult.ClearSlots();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (this.item != null)
            {
                tooltip.GenerateToolTip(this.item);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tooltip.gameObject.SetActive(false);
        }

        private void ClearSlot()
        {
            this.item = null;
            itemIconImage.color = Color.clear;
        }
    }
}