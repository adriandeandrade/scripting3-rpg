using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using enjoii.Items;
using enjoii.Items.Slot;

public class EquipmentPanel : MonoBehaviour
{
    // Private Variables
    private List<BaseItemSlot> equipmentSlots;

    private void Awake()
    {
        equipmentSlots = GetComponent<SlotPanel>().ItemSlots;
        equipmentSlots.ForEach(i => i.SlotType = SlotType.EquippableSlot);
    }

    public void UpdateEquippableSlot(int slotIndex, EquipmentItem item)
    {
        equipmentSlots[slotIndex].UpdateSlot(item);
    }
}
