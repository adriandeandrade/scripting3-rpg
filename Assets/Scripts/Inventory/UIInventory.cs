using enjoii.Items.Slot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace enjoii.Items
{
    public class UIInventory : MonoBehaviour
    {
        // Inspecotr Fields
        [SerializeField] private SlotPanel[] slotPanels;

        public void AddItemToUI(Item item)
        {
            foreach (SlotPanel slotPanel in slotPanels)
            {
                if (slotPanel.ContainsEmptySlot())
                {
                    slotPanel.AddNewItem(item);
                    break;
                }
            }
        }

        public void ClearItems()
        {
            foreach(SlotPanel slotPanel in slotPanels)
            {
                foreach (BaseItemSlot itemSlot in slotPanel.ItemSlots)
                {
                    itemSlot.UpdateSlot(null);
                }
            }
        }
    }
}
