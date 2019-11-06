using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace enjoii.Stats
{
    public enum StatModifierType
    {
        Flat = 100,
        PercentAdd = 200,
        PercentMultiply = 300
    }

    public class StatModifier
    {
        public readonly float Value;
        public readonly StatModifierType StatModifierType;
        public readonly int Order;
        public readonly object Source;

        public StatModifier(float value, StatModifierType _statModifierType, int order, object source)
        {
            Value = value;
            StatModifierType = _statModifierType;
            Order = order;
            Source = source;
        }

        public StatModifier(float value, StatModifierType type) : this(value, type, (int)type, null) { }
        public StatModifier(float value, StatModifierType type, int order) : this(value, type, order, null) { }
        public StatModifier(float value, StatModifierType type, object source) : this(value, type, (int)type, source) { }
    }
}
