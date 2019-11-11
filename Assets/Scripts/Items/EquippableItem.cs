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
        [SerializeField] private int agilityBonus;
        [SerializeField] private int vitalityBonus;
        [Space]
        [SerializeField] private float strengthPercentBonus;
        [SerializeField] private float agilityPercentBonus;
        [SerializeField] private float vitalityPercentBonus;
        public GameObject projectilePrefab;
        public Sprite projectileIcon;

        // Properties
        public EquipmentTypes EquipmentType => equipmentType;
        public int StrengthBonus => strengthBonus;
        public int AgilityBonus => agilityBonus;
        public int VitalityBonus => vitalityBonus;
        public float StrengthPercentBonus => strengthPercentBonus;
        public float AgilityPercentBonus => agilityPercentBonus;
        public float VitalityPercentBonus => vitalityPercentBonus;

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
            Debug.Log("Equipped an item.");
            //if(strengthBonus != 0)
            //{
            //    character.strengthStat.AddModifier(new StatModifier(strengthBonus, StatModifierType.Flat, this));
            //}

            //if (agilityBonus != 0)
            //{
            //    character.agilityStat.AddModifier(new StatModifier(agilityBonus, StatModifierType.Flat, this));
            //}

            //if (vitalityBonus != 0)
            //{
            //    character.vitalityStat.AddModifier(new StatModifier(vitalityBonus, StatModifierType.Flat, this));
            //}

            //if(strengthPercentBonus != 0)
            //{
            //    character.strengthStat.AddModifier(new StatModifier(strengthPercentBonus, StatModifierType.PercentMultiply, this));
            //}

            //if (agilityPercentBonus != 0)
            //{
            //    character.agilityStat.AddModifier(new StatModifier(agilityPercentBonus, StatModifierType.PercentMultiply, this));
            //}

            //if (vitalityPercentBonus != 0)
            //{
            //    character.vitalityStat.AddModifier(new StatModifier(vitalityPercentBonus, StatModifierType.PercentMultiply, this));
            //}
        }

        public void UnEquip(Player character)
        {
            Debug.Log("Un-Equipped an item.");
            //character.strengthStat.RemoveAllModifiersFromSource(this);
            //character.agilityStat.RemoveAllModifiersFromSource(this);
            //character.vitalityStat.RemoveAllModifiersFromSource(this);
        }

        public override string GetItemType()
        {
            return equipmentType.ToString();
        }

        public override string GetDescription()
        {
            sb.Length = 0;
            AddStat(strengthBonus, "Strength");
            AddStat(agilityBonus, "Agility");
            AddStat(vitalityBonus, "Vitality");

            AddStat(strengthPercentBonus, "Strength", isPercent: true);
            AddStat(agilityPercentBonus, "Agility", isPercent: true);
            AddStat(vitalityPercentBonus, "Vitality", isPercent: true);

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
