using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace enjoii.Items
{
    public class UICraftResult : MonoBehaviour
    {
        // Inspector Fields
        [SerializeField] private SlotPanel slotPanel;

        // Private Variables
        private Inventory inventory;

        private void Start()
        {
            inventory = GameManager.Instance.PlayerRef.Inventory;
        }

        public void PickItem()
        {
            inventory.Items.Add(GetComponent<ItemSlot>().ItemInSlot);
        }

        public void ClearSlots()
        {
            slotPanel.EmptyAllSlots();
        }

    }
}
