using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace enjoii.Items
{
    public class SlotPanel : MonoBehaviour
    {
        // Inspector Fields
        [SerializeField] private int slotAmount;
        [SerializeField] private SlotType slotType;
        private const string slotPrefabPath = "Prefabs/Slot";

        // Properties
        public List<ItemSlot> ItemSlots { get; } = new List<ItemSlot>();

        private void Awake()
        {
            for (int i = 0; i < slotAmount; i++)
            {
                GameObject newSlot = Instantiate(Resources.Load<GameObject>(slotPrefabPath));
                ItemSlot slotUIItem = newSlot.GetComponentInChildren<ItemSlot>();
                slotUIItem.SlotType = slotType;

                newSlot.transform.SetParent(transform);
                ItemSlots.Add(slotUIItem);
                ItemSlots[i].ItemInSlot = null;
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        }

        public void UpdateSlot(int slot, Item item)
        {
            ItemSlots[slot].UpdateSlot(item);
        }

        public void AddNewItem(Item item)
        {
            UpdateSlot(ItemSlots.FindIndex(i => i.ItemInSlot == null), item);
        }

        public void RemoveItem(Item item)
        {
            UpdateSlot(ItemSlots.FindIndex(i => i.ItemInSlot == item), null);
        }

        public void EmptyAllSlots()
        {
            ItemSlots.ForEach(i => i.UpdateSlot(null));
        }

        public bool ContainsEmptySlot()
        {
            foreach (ItemSlot uIItem in ItemSlots)
            {
                if (uIItem.ItemInSlot == null) return true;
            }

            return false;
        }
    }
}
