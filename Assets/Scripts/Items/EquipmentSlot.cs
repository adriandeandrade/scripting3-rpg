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

        protected override void OnValidate()
        {
            base.OnValidate();
            gameObject.name = slotEquipmentType.ToString() + " Slot";
        }

        public override bool CanRecieveItem(Item item)
        {
            if (ItemInSlot == null) return true;

            EquippableItem equippableItem = item as EquippableItem;
            return equippableItem != null && equippableItem.EquipmentType == slotEquipmentType;
        }
    }
}
