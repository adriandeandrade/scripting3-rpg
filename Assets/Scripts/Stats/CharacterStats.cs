using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats
{
    public List<BaseStat> stats = new List<BaseStat>();

    public CharacterStats(int strength, int vitality)
    {
        stats = new List<BaseStat>()
        {
            new BaseStat(StatTypes.Strength, strength, "My Power"),
            new BaseStat(StatTypes.Vitality, vitality, "My Vitality")
        };
    }

    public BaseStat GetStat(StatTypes stat)
    {
        return this.stats.Find(x => x.StatType == stat);
    }

    public void AddStatModifier(List<BaseStat> statBonuses)
    {
        foreach (BaseStat bonus in statBonuses)
        {
            GetStat(bonus.StatType).AddStatModifier(new StatBonus(bonus.BaseValue));
        }
    }

    public void RemoveStatModifier(List<BaseStat> statBonuses)
    {
        foreach (BaseStat bonus in statBonuses)
        {
            stats.Find(x => x.StatName == bonus.StatName).RemoveStatModifier(new StatBonus(bonus.BaseValue));
        }
    }

}
