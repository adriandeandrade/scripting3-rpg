using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace enjoii.Items
{
    public abstract class ItemContainer : MonoBehaviour, IItemContainer
    {
        // Inspector Fields
        [SerializeField] private List<ItemSlot> itemSlots;

        // Properties
        public List<ItemSlot> ItemSlots => itemSlots;

        // Events
        public event Action<BaseItemSlot> OnPointerEnterEvent;
        public event Action<BaseItemSlot> OnPointerExitEvent;
        public event Action<BaseItemSlot> OnRightClickEvent;
        public event Action<BaseItemSlot> OnBeginDragEvent;
        public event Action<BaseItemSlot> OnEndDragEvent;
        public event Action<BaseItemSlot> OnDragEvent;
        public event Action<BaseItemSlot> OnDropEvent;

        protected virtual void OnValidate()
        {
            GetComponentsInChildren(includeInactive: true, result: itemSlots);
        }

        protected virtual void Awake()
        {
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                ItemSlots[i].OnPointerEnterEvent += slot => EventHelper(slot, OnPointerEnterEvent);
                ItemSlots[i].OnPointerExitEvent += slot => EventHelper(slot, OnPointerExitEvent);
                ItemSlots[i].OnRightClickEvent += slot => EventHelper(slot, OnRightClickEvent);
                ItemSlots[i].OnBeginDragEvent += slot => EventHelper(slot, OnBeginDragEvent);
                ItemSlots[i].OnEndDragEvent += slot => EventHelper(slot, OnEndDragEvent);
                ItemSlots[i].OnDragEvent += slot => EventHelper(slot, OnDragEvent);
                ItemSlots[i].OnDropEvent += slot => EventHelper(slot, OnDropEvent);
            }
        }

        private void EventHelper(BaseItemSlot itemSlot, Action<BaseItemSlot> action)
        {
            action?.Invoke(itemSlot);
        }

        public virtual bool AddItem(Item item)
        {
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                if (ItemSlots[i].CanAddStack(item))
                {
                    ItemSlots[i].ItemInSlot = item;
                    ItemSlots[i].ItemQuantity++;
                    return true;
                }
            }

            for (int i = 0; i < ItemSlots.Count; i++)
            {
                if (ItemSlots[i].ItemInSlot == null)
                {
                    ItemSlots[i].ItemInSlot = item;
                    ItemSlots[i].ItemQuantity++;
                    return true;
                }
            }

            return false;
        }

        public virtual bool CanAddItem(Item item, int itemQuantity = 1)
        {
            int spaceRemainingSlot = 0;

            foreach (ItemSlot itemSlot in ItemSlots)
            {
                if (itemSlot.ItemInSlot == null || itemSlot.ItemInSlot.ItemID == item.ItemID)
                {
                    spaceRemainingSlot += item.MaxStackSize - itemSlot.ItemQuantity;
                }
            }

            return spaceRemainingSlot >= itemQuantity;
        }

        public void ClearContainer()
        {
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                if (ItemSlots[i].ItemInSlot != null && Application.isPlaying)
                {
                    ItemSlots[i].ItemInSlot.Destroy();
                }

                ItemSlots[i].ItemInSlot = null;
                ItemSlots[i].ItemQuantity = 0;
            }
        }

        public virtual int ItemCount(string itemID)
        {
            int count = 0;

            for (int i = 0; i < ItemSlots.Count; i++)
            {
                Item item = ItemSlots[i].ItemInSlot;

                if (item != null && item.ItemID == itemID)
                {
                    count += ItemSlots[i].ItemQuantity;
                }
            }

            return count;
        }

        public virtual Item RemoveItem(string itemID)
        {
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                Item item = ItemSlots[i].ItemInSlot;
                if (item != null && item.ItemID == itemID)
                {
                    ItemSlots[i].ItemQuantity--;
                    return item;
                }
            }

            return null;
        }

        public bool RemoveItem(Item item)
        {
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                if (ItemSlots[i].ItemInSlot == item)
                {
                    ItemSlots[i].ItemQuantity--;
                    Debug.Log($"{item.ItemName} has been removed.");

                    return true;
                }
            }

            return false;
        }
    }
}
