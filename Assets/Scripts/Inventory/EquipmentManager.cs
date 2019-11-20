using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using enjoii.Items;

public class EquipmentManager : MonoBehaviour
{
    public EquipmentItem[] defaultEquipment;
    public EquipmentItem[] currentEquipment;

    private Inventory inventory;
    public EquipmentPanel equipmentPanel;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        equipmentPanel = FindObjectOfType<EquipmentPanel>();
    }
    private void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipmentType)).Length;
        currentEquipment = new EquipmentItem[numSlots];
    }

    public void Equip(EquipmentItem item)
    {
        int slotIndex = (int)item.equipmentType;
        EquipmentItem oldItem = Unequip(slotIndex);

        if(currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.GiveItem(oldItem.id);
        }

        currentEquipment[slotIndex] = item;
        inventory.RemoveItem(item.id);
        equipmentPanel.UpdateEquippableSlot(slotIndex, item);
    }

    public EquipmentItem Unequip(int slotIndex)
    {
        EquipmentItem oldItem = null;

        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.GiveItem(oldItem.id);

            currentEquipment[slotIndex] = null;
            equipmentPanel.UpdateEquippableSlot(slotIndex, null);
        }

        return oldItem;
    }

    public int GetSlotIndex(EquipmentItem item)
    {
        return (int)item.equipmentType;
    }
}
