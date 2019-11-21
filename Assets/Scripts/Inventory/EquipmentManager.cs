using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using enjoii.Items;

namespace enjoii.Items
{
    public class EquipmentManager : MonoBehaviour
    {
        // Inspector Fields
        [SerializeField] private EquipmentPanel equipmentPanel;

        // Private Variables
        private EquipmentItem[] currentEquipment;
        private Inventory inventory;
        private WeaponController weaponController;

        // Event
        //public System.Action<EquipmentItem, EquipmentItem> OnEquipmentChanged;

        private void Awake()
        {
            weaponController = GetComponent<WeaponController>();
            inventory = GameManager.Instance.PlayerRef.Inventory;
            int numSlots = System.Enum.GetNames(typeof(EquipmentType)).Length;
            currentEquipment = new EquipmentItem[numSlots];
        }

        private void Start()
        {
            
        }

        public void Equip(EquipmentItem item)
        {
            if (item == null)
            {
                return;
            }

            int slotIndex = (int)item.equipmentType;
            EquipmentItem oldItem = Unequip(slotIndex);

            if(item.equipmentType == EquipmentType.Weapon)
            {
                if(weaponController == null)
                {
                    Debug.Log("Weapon controller not found.");
                    return;
                }

                weaponController.EquipWeapon(item);
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
                weaponController.UnEquipWeapon();
                equipmentPanel.UpdateEquippableSlot(slotIndex, null);
            }

            return oldItem;
        }

        public int GetSlotIndex(EquipmentItem item)
        {
            return (int)item.equipmentType;
        }

        public void UnequipAll()
        {
            if (currentEquipment == null || currentEquipment.Length == 0) return;

            for (int i = 0; i < currentEquipment.Length; i++)
            {
                Unequip(i);
            }
        }
    }
}

