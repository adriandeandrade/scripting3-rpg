using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace enjoii.Stats
{
    [Serializable]
    public class CharacterStat
    {
        // Public Variables
        public float BaseValue;

        public readonly ReadOnlyCollection<StatModifier> StatModifiers;

        // Private Variables
        protected readonly List<StatModifier> statModifiers;

        protected bool isDirty = true;
        protected float lastBaseValue;
        protected float value;

        // Properties
        public virtual float Value
        {
            get
            {
                if(isDirty || lastBaseValue != BaseValue)
                {
                    lastBaseValue = BaseValue;
                    value = CalculateValue();
                    isDirty = false;
                }
                return value;
            }
        }

        public CharacterStat()
        {
            statModifiers = new List<StatModifier>();
            StatModifiers = statModifiers.AsReadOnly();
        }

        public CharacterStat(float baseValue) : this()
        {
            BaseValue = baseValue;
        }

        public virtual void AddModifier(StatModifier modifier)
        {
            isDirty = true;
            statModifiers.Add(modifier);
            statModifiers.Sort(CompareModifierOrder);
        }

        public virtual bool RemoveModifier(StatModifier modifier)
        {
            if(statModifiers.Remove(modifier))
            {
                isDirty = true;
                return true;
            }

            return false;
        }

        public virtual bool RemoveAllModifiersFromSource(object source)
        {
            bool didRemove = false;

            for (int i = statModifiers.Count - 1; i >= 0; i--)
            {
                if(statModifiers[i].Source == source)
                {
                    isDirty = true;
                    didRemove = true;
                    statModifiers.RemoveAt(i);
                }
            }

            return didRemove;
        }

        protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order)
                return -1;
            else if (a.Order > b.Order)
                return 1;

            return 0;
        }

        protected virtual float CalculateValue()
        {
            float finalValue = BaseValue;
            float sumPercentAdd = 0;

            for (int i = 0; i < statModifiers.Count; i++)
            {
                StatModifier modifier = statModifiers[i];

                if (modifier.StatModifierType == StatModifierType.Flat)
                {
                    finalValue += modifier.Value;
                }
                else if (modifier.StatModifierType == StatModifierType.PercentAdd)
                {
                    sumPercentAdd += modifier.Value;

                    if (i + 1 >= statModifiers.Count || statModifiers[i + 1].StatModifierType != StatModifierType.PercentAdd)
                    {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }
                else if (modifier.StatModifierType == StatModifierType.PercentMultiply)
                {
                    finalValue *= 1 + modifier.Value;
                }
            }

            return (float)Math.Round(finalValue, 4);
        }
    }
}
