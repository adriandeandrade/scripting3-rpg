using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using enjoii.Items;

public interface IWeapon
{
    List<BaseStat> WeaponStats { get; set; }
    EquippableItem EquippableItemData { get; }
    int CurrentDamage { get; set; }
    void PerformAttack(int damage);
    void PerformSpecialAttack();
}
