using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace enjoii.Items
{
    public class ItemSlot : BaseItemSlot, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
    {
        // Private Variables
        private bool isDragging = false;
        private Color dragColor = new Color(1, 1, 1, 0.5f);

        // Events
        public event Action<BaseItemSlot> OnBeginDragEvent = null;
        public event Action<BaseItemSlot> OnEndDragEvent = null;
        public event Action<BaseItemSlot> OnDragEvent = null;
        public event Action<BaseItemSlot> OnDropEvent = null;

        public override bool CanAddStack(Item item, int _itemQuantity = 1)
        {
            return base.CanAddStack(item, _itemQuantity) && ItemQuantity + _itemQuantity <= ItemInSlot.MaxStackSize;
        }

        public override bool CanRecieveItem(Item item)
        {
            return true;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (isDragging)
            {
                OnEndDrag(null);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            isDragging = true;

            if (ItemInSlot != null)
            {
                itemIconImage.color = dragColor;
            }

            if (OnBeginDragEvent != null)
            {
                OnBeginDragEvent(this);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isDragging = false;

            if (ItemInSlot != null)
            {
                itemIconImage.color = originalColor;
            }

            if (OnEndDragEvent != null)
            {
                OnEndDragEvent(this);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (OnDragEvent != null)
            {
                OnDragEvent(this);
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (OnDropEvent != null)
            {
                OnDropEvent(this);
            }
        }
    }
}
