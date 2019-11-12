using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using enjoii.Characters;
using enjoii.Stats;

namespace enjoii.Items
{
    [CreateAssetMenu(fileName = "New Equippable Item", menuName = "Items/New Equippable Item")]
    public class EquippableItem : Item
    {
        // Inspector Fields
        [Header("Equippable Item Configuration")]
        [SerializeField] private EquipmentTypes equipmentType;
        [SerializeField] private int strengthBonus;
        [Space]
        [SerializeField] private float strengthPercentBonus;

        public GameObject projectilePrefab;
        public Sprite projectileIcon;

        // Properties
        public EquipmentTypes EquipmentType => equipmentType;
        public int StrengthBonus => strengthBonus;
        public float StrengthPercentBonus => strengthPercentBonus;

        public override Item GetCopy()
        {
            return Instantiate(this);
        }

        public override void Destroy()
        {
            Destroy(this);
        }

        public void Equip(Player character)
        {
            if (strengthBonus != 0)
            {
                character.strengthStat.AddModifier(new StatModifier(strengthBonus, StatModifierType.Flat, this));
                Debug.Log(strengthBonus);
            }

            if (strengthPercentBonus != 0)
            {
                character.strengthStat.AddModifier(new StatModifier(strengthPercentBonus, StatModifierType.PercentMultiply, this));
            }

        }

        public void UnEquip(Player character)
        {
            Debug.Log("Un-Equipped an item.");
            character.strengthStat.RemoveAllModifiersFromSource(this);
        }

        public override string GetItemType()
        {
            return equipmentType.ToString();
        }

        public override string GetDescription()
        {
            sb.Length = 0;

            AddStat(strengthBonus, "Strength");
            AddStat(strengthPercentBonus, "Strength", isPercent: true);

            return sb.ToString();
        }

        private void AddStat(float value, string statName, bool isPercent = false)
        {
            if (value != 0)
            {
                if (sb.Length > 0)
                    sb.AppendLine();

                if (value > 0)
                    sb.Append("+");

                if (isPercent)
                {
                    sb.Append(value * 100);
                    sb.Append("% ");
                }
                else
                {
                    sb.Append(value);
                    sb.Append(" ");
                }
                sb.Append(statName);
            }
        }
    }
}
