using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using enjoii.Items.Slot;

namespace enjoii.Items
{
    public class SlotPanel : MonoBehaviour
    {
        // Inspector Fields
        [SerializeField] private int slotAmount;
        [SerializeField] private SlotType slotType;
        private const string slotPrefabPath = "Prefabs/Slots/";

        // Properties
        public List<BaseItemSlot> ItemSlots { get; } = new List<BaseItemSlot>();

        private void Awake()
        {
            InitializePanel();
        }
        private void InitializePanel()
        {
            GameObject slotPrefab = Resources.Load<GameObject>($"{slotPrefabPath}{slotType.ToString()}");

            for (int i = 0; i < slotAmount; i++)
            {
                GameObject newSlot = Instantiate(slotPrefab);
                BaseItemSlot slotUIItem = newSlot.GetComponentInChildren<BaseItemSlot>();
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
            foreach (BaseItemSlot itemSlot in ItemSlots)
            {
                if (itemSlot.ItemInSlot == null) return true;
            }

            return false;
        }
    }
}
