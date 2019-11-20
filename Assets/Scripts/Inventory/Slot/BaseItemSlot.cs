using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

namespace enjoii.Items
{
    public enum SlotType
    {
        ItemSlot,
        CraftingSlot,
        CraftingResultSlot,
        EquippableSlot,
        TrashSlot,
        StorageSlot
    }
}

namespace enjoii.Items.Slot
{
    public abstract class BaseItemSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        // Inspector Fields
        [SerializeField] protected Image itemIconImage;
        [SerializeField] protected Image slotImage;
        [SerializeField] protected SlotType slotType;

        // Private Variables
        private Item itemInSlot;
        protected BaseItemSlot selectedItem;
        private bool pointerOver = false;

        // Components
        private Tooltip tooltip;

        // Properties
        public Item ItemInSlot { get => itemInSlot; set => itemInSlot = value; }
        public SlotType SlotType { get => slotType; set => slotType = value; }

        // Events
        public Action<BaseItemSlot> OnSlotClickedEvent;
        public Action<BaseItemSlot> OnMouseEnterSlotEvent;
        public Action<BaseItemSlot> OnMouseExitSlotEvent;

        protected virtual void Awake()
        {
            tooltip = FindObjectOfType<Tooltip>();
            selectedItem = GameObject.Find("HandSlot").GetComponent<BaseItemSlot>();
        }

        private void OnDisable()
        {
            if (pointerOver) OnPointerExit(null);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnSlotClickedEvent?.Invoke(this);
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            OnMouseEnterSlotEvent?.Invoke(this);

            if(ItemInSlot != null)
            {
                tooltip.GenerateToolTip(ItemInSlot);
            }
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            OnMouseExitSlotEvent?.Invoke(this);
            tooltip.gameObject.SetActive(false);
        }

        public virtual void UpdateSlot(Item item)
        {
            if(item == null)
            {
                ClearSlot();
                return;
            }

            itemInSlot = item;

            if(ItemInSlot != null)
            {
                itemIconImage.color = Color.white;
                itemIconImage.sprite = item.icon;
            }
        }

        public void ClearSlot()
        {
            itemIconImage.color = Color.clear;
            ItemInSlot = null;
        }
    }
}