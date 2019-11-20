using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using enjoii.Items.Slot;

namespace enjoii.Items
{
    public class StoragePanel : MonoBehaviour
    {
        // Private Variables
        private List<BaseItemSlot> storageSlots;

        private void Awake()
        {
            storageSlots = GetComponent<SlotPanel>().ItemSlots;
            storageSlots.ForEach(i => i.SlotType = SlotType.StorageSlot);
        }
    }
}
