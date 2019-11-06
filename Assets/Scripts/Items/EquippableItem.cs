using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using enjoii.Characters;

namespace enjoii.Items
{
    [CreateAssetMenu(fileName = "New Equippable Item", menuName = "New Equippable Item")]
    public class EquippableItem : Item
    {
        // Inspector Fields
        [Header("Equippable Item Configuration")]
        [SerializeField] private EquipmentTypes equipmentType;

        // Properties
        public EquipmentTypes EquipmentType => equipmentType;

        public override Item GetCopy()
        {
            return Instantiate(this);
        }

        public override void Destroy()
        {
            Destroy(this);
        }

        public void Equip(Character character)
        {
            Debug.Log("Item Equipped");
        }

        public void UnEquip(Character character)
        {
            Debug.Log("Item UnEquipped.");
        }

        public override string GetItemType()
        {
            return equipmentType.ToString();
        }

        public override string GetDescription()
        {
            sb.Length = 0;
            sb.AppendLine("CHANGE SO THIS DESCRIBES THE EQUIPPABLE!");
            return sb.ToString();
        }
    }
}
