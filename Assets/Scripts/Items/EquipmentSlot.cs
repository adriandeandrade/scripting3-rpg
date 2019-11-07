using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace enjoii.Items
{
    public class EquipmentSlot : ItemSlot
    {
        // Inspector Fields
        [Header("Equipment Slot Configuration")]
        [SerializeField] private EquipmentTypes slotEquipmentType;

        // Properties
        public EquipmentTypes SlotEquipmentType => slotEquipmentType;

        protected override void OnValidate()
        {
            ItemInSlot = itemInSlot;
            ItemQuantity = itemQuantity;

            gameObject.name = slotEquipmentType.ToString() + " Slot";
        }

        public override bool CanRecieveItem(Item item)
        {
            if (item == null) return true;

            EquippableItem equippableItem = item as EquippableItem;
            return equippableItem != null && equippableItem.EquipmentType == slotEquipmentType;
        }
    }
}
