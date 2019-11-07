using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

namespace enjoii.Items
{
    public class BaseItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        // Inspector Fields
        [Header("Base Slot Configuration")]
        [SerializeField] protected Image itemIconImage;
        [SerializeField] protected TextMeshProUGUI quantityText;

        // Private Variables
        protected bool isMouseOverSlot = false;
        protected Color originalColor = Color.white;
        protected Color disabledColor = new Color(1, 1, 1, 0);
        protected Item itemInSlot = null;
        protected int itemQuantity;

        // Events
        public event Action<BaseItemSlot> OnPointerEnterEvent = null;
        public event Action<BaseItemSlot> OnPointerExitEvent = null;
        public event Action<BaseItemSlot> OnRightClickEvent = null;

        // Properties
        public Item ItemInSlot
        {
            get
            {
                return itemInSlot;
            }

            set
            {
                itemInSlot = value;

                if (itemInSlot == null && ItemQuantity != 0) ItemQuantity = 0;

                if (itemInSlot == null)
                {
                    itemIconImage.sprite = null;
                    itemIconImage.color = disabledColor;
                }
                else
                {
                    itemIconImage.sprite = itemInSlot.ItemIcon;
                    itemIconImage.color = originalColor;
                }

                if (isMouseOverSlot)
                {
                    OnPointerExit(null);
                    OnPointerEnter(null);
                }
            }
        }

        public int ItemQuantity
        {
            get
            {
                return itemQuantity;
            }

            set
            {
                itemQuantity = value;
                if (itemQuantity < 0) itemQuantity = 0;
                if (itemQuantity == 0 && ItemInSlot != null) ItemInSlot = null;

                if (quantityText != null)
                {
                    quantityText.enabled = itemInSlot != null && itemQuantity > 1;
                    if (quantityText.enabled)
                    {
                        quantityText.SetText(itemQuantity.ToString());
                    }
                }
            }
        }

        public virtual bool CanAddStack(Item item, int _itemQuantity = 1)
        {
            return ItemInSlot != null && ItemInSlot.ItemID == item.ItemID;
        }

        public virtual bool CanRecieveItem(Item item)
        {
            return false;
        }

        protected virtual void OnValidate()
        {
            if (itemIconImage == null)
            {
                itemIconImage = GetComponentInChildren<Image>();
            }

            if (quantityText == null)
            {
                quantityText = GetComponentInChildren<TextMeshProUGUI>();
            }

            ItemInSlot = itemInSlot;
            ItemQuantity = itemQuantity;
        }

        protected virtual void OnDisable()
        {
            if (isMouseOverSlot) OnPointerExit(null);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
            {
                OnRightClickEvent?.Invoke(this);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isMouseOverSlot = true;
            OnPointerEnterEvent?.Invoke(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isMouseOverSlot = false;
            OnPointerExitEvent?.Invoke(this);
        }
    }
}


