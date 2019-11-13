using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatTypes 
{ 
    Strength, 
    Vitality
}

[System.Serializable]
public class BaseStat
{
    // Private Variable
    [SerializeField] private StatTypes statType;
    [SerializeField] private List<StatBonus> statModifiers;

    private string statName;
    [SerializeField] private string statDescription;
    [SerializeField] private int baseValue;
    private int value;

    // Properties
    public StatTypes StatType => statType;
    public List<StatBonus> Modifiers { get => statModifiers; set => statModifiers = value; }
    public string StatName => statName;
    public string StatDescription => statDescription;
    public int BaseValue => baseValue;
    public int Value => value;

    public BaseStat(StatTypes _statType, int _baseValue, string _statDescription)
    {
        statModifiers = new List<StatBonus>();
        statType = _statType;
        baseValue = _baseValue;
        statName = statType.ToString();
        statDescription = _statDescription;
    }

    public void AddStatModifier(StatBonus statModifier)
    {
        statModifiers.Add(statModifier);
    }

    public void RemoveStatModifier(StatBonus statModifier)
    {
        statModifiers.Remove(statModifiers.Find(x => x.ModifierValue == statModifier.ModifierValue));
    }

    public int CalculateValue()
    {
        value = 0;

        foreach (StatBonus statBonus in statModifiers)
        {
            value += statBonus.ModifierValue;
        }

        value += baseValue;

        return value;
    }
}
