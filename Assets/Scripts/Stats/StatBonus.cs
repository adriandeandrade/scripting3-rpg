using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatBonus
{
    [SerializeField] private int modifierValue;
    public int ModifierValue => modifierValue;

    public StatBonus(int _modifierValue)
    {
        modifierValue = _modifierValue;
        Debug.Log("New Stat Bonus initiated.");
    }
}
