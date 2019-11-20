using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace enjoii.Items
{
    public enum EquipmentType
    {
        Weapon,
        Helmet,
        Boots
    }

    [System.Serializable]
    public class EquipmentItem : Item
    {
        public EquipmentType equipmentType;

        public EquipmentItem(int id, string name, string description, string fileName, EquipmentType itemType, Dictionary<string, int> stats) :
            base(id, name, description, fileName, stats)
        {
            this.equipmentType = itemType;
        }

        public EquipmentItem(EquipmentItem equipmentItem) : base(equipmentItem)
        {
            equipmentType = equipmentItem.equipmentType;
        }
    }
}


