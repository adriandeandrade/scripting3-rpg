using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using enjoii.Items;

public class EquipmentPanel : MonoBehaviour
{
    // Inspector Fields
    [SerializeField] private ItemDatabase itemDatabase;

    // Private Variables
    private List<ItemSlot> equipmentSlots;

    private void Awake()
    {
        equipmentSlots = GetComponent<SlotPanel>().itemSlots;
        equipmentSlots.ForEach(i => i.slotType = SlotType.EquippableSlot);
    }

    private void Start()
    {
        itemDatabase = GameManager.Instance.ItemDatabase;
    }

    public void UpdateEquippableSlot(int slotIndex, EquipmentItem item)
    {
        equipmentSlots[slotIndex].UpdateSlot(item);
    }
}
