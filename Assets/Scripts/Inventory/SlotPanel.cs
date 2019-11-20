using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace enjoii.Items
{
    public class SlotPanel : MonoBehaviour
    {
        public List<ItemSlot> itemSlots = new List<ItemSlot>();
        public int slotAmount;
        public SlotType slotType;

        private const string slotPrefabPath = "Prefabs/Slot";

        private void Awake()
        {
            for (int i = 0; i < slotAmount; i++)
            {
                GameObject newSlot = Instantiate(Resources.Load<GameObject>(slotPrefabPath));
                ItemSlot slotUIItem = newSlot.GetComponentInChildren<ItemSlot>();
                slotUIItem.slotType = slotType;

                newSlot.transform.SetParent(transform);
                itemSlots.Add(slotUIItem);
                itemSlots[i].item = null;
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        }

        public void UpdateSlot(int slot, Item item)
        {
            itemSlots[slot].UpdateSlot(item);
        }

        public void AddNewItem(Item item)
        {
            UpdateSlot(itemSlots.FindIndex(i => i.item == null), item);
        }

        public void RemoveItem(Item item)
        {
            UpdateSlot(itemSlots.FindIndex(i => i.item == item), null);
        }

        public void EmptyAllSlots()
        {
            itemSlots.ForEach(i => i.UpdateSlot(null));
        }

        public bool ContainsEmptySlot()
        {
            foreach (ItemSlot uIItem in itemSlots)
            {
                if (uIItem.item == null) return true;
            }

            return false;
        }
    }
}
