using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace enjoii.Items
{
    public class EquipmentPanel : MonoBehaviour
    {
        // Inspector Fields
        [Header("Equipment Container Configuration")]
        [SerializeField] private EquipmentSlot[] equipmentSlots;
        [SerializeField] private Transform equipmentSlotsParent;

        // Properties
        public EquipmentSlot[] EquipmentSlots => equipmentSlots;

        // Events
        public event Action<BaseItemSlot> OnPointerEnterEvent;
        public event Action<BaseItemSlot> OnPointerExitEvent;
        public event Action<BaseItemSlot> OnRightClickEvent;
        public event Action<BaseItemSlot> OnBeginDragEvent;
        public event Action<BaseItemSlot> OnEndDragEvent;
        public event Action<BaseItemSlot> OnDragEvent;
        public event Action<BaseItemSlot> OnDropEvent;

        private void Start()
        {
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                EquipmentSlots[i].OnPointerEnterEvent += slot => OnPointerEnterEvent(slot);
                EquipmentSlots[i].OnPointerExitEvent += slot => OnPointerExitEvent(slot);
                EquipmentSlots[i].OnRightClickEvent += slot => OnRightClickEvent(slot);
                EquipmentSlots[i].OnBeginDragEvent += slot => OnBeginDragEvent(slot);
                EquipmentSlots[i].OnEndDragEvent += slot => OnEndDragEvent(slot);
                EquipmentSlots[i].OnDragEvent += slot => OnDragEvent(slot);
                EquipmentSlots[i].OnDropEvent += slot => OnDropEvent(slot);
            }
        }

        private void OnValidate()
        {
            equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
        }

        public bool EquipItem(EquippableItem item, out EquippableItem previousItem)
        {
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                if(equipmentSlots[i].SlotEquipmentType == item.EquipmentType)
                {
                    previousItem = (EquippableItem)equipmentSlots[i].ItemInSlot;
                    equipmentSlots[i].ItemInSlot = item;
                    equipmentSlots[i].ItemQuantity = 1;
                    return true;
                }
            }

            previousItem = null;
            return false;
        }

        public bool UnEquipItem(EquippableItem item)
        {
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                if(equipmentSlots[i].ItemInSlot == item)
                {
                    equipmentSlots[i].ItemInSlot = null;
                    equipmentSlots[i].ItemQuantity = 0;
                    return true;
                }
            }

            return false;
        }
    }
}

