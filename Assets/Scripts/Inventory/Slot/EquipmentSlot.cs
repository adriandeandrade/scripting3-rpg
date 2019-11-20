using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace enjoii.Items.Slot
{
    public class EquipmentSlot : BaseItemSlot
    {
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            if (ItemInSlot == null) return;

            if(eventData != null && eventData.button == PointerEventData.InputButton.Right)
            {
                EquipmentItem equippable = ItemInSlot as EquipmentItem;
                
                int slotIndex = GameManager.Instance.PlayerRef.EquipmentManager.GetSlotIndex(equippable); // Get the slot index of the equipment item we want to remove.
                GameManager.Instance.PlayerRef.EquipmentManager.Unequip(slotIndex); // Unequip the item.

                UpdateSlot(null); // Clear the equipment slot.
            }
        }
    }
}


