using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace enjoii.Items
{
    public class UICraftResult : MonoBehaviour
    {
        public SlotPanel slotPanel;
        public Inventory inventory;

        public void PickItem()
        {
            inventory.items.Add(GetComponent<ItemSlot>().item);
        }

        public void ClearSlots()
        {
            slotPanel.EmptyAllSlots();
        }

    }
}
