using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using enjoii.Items;
using System.Text;

public class PlayerCharacterStats : CharacterStats
{
    [SerializeField] private TextMeshProUGUI statsText;

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
            Debug.Log(powerStat.GetValue());
            Debug.Log(defenceStat.GetValue());
        }
    }

    public void UpdateStatText()
    {
        StringBuilder sb = new StringBuilder();

        int powerValue = powerStat.GetValue();
        int defenceValue = defenceStat.GetValue();

        sb.AppendLine($"Power: {powerValue}");
        sb.AppendLine($"Defence: {defenceValue}");

        statsText.SetText(sb.ToString());
    }
}
