using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using enjoii.Items;

public class PlayerCharacterStats : CharacterStats
{
    public void AddModifiers(EquipmentItem item)
    {
        if (item != null)
        {
            powerStat.AddModifier(item.stats["Power"]);
            defenceStat.AddModifier(item.stats["Defence"]);
        }
    }

    public void RemoveModifiers(EquipmentItem item)
    {
        if (item != null)
        {
            powerStat.RemoveModifier(item.stats["Power"]);
            defenceStat.RemoveModifier(item.stats["Defence"]);
        }

        Debug.Log(powerStat.GetValue());
        Debug.Log(defenceStat.GetValue());
    }
}
