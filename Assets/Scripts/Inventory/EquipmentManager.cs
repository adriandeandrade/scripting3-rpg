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
        }

        private void Start()
        {
            int numSlots = System.Enum.GetNames(typeof(EquipmentType)).Length;
            currentEquipment = new EquipmentItem[numSlots];

            inventory = GameManager.Instance.PlayerRef.Inventory;
        }

        public void Equip(EquipmentItem item)
        {
            int slotIndex = (int)item.equipmentType;
            EquipmentItem oldItem = Unequip(slotIndex);

            if(item.equipmentType == EquipmentType.Weapon)
            {
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
    }
}

