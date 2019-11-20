using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using enjoii.Items;

public interface IWeapon
{
    List<BaseStat> WeaponStats { get; set; }
    //EquippableItem WeaponData { get; }
    int CurrentDamage { get; set; }
    int ItemID { get; set; }
    void PerformAttack(int damage);
    void PerformSpecialAttack();
    void OnWeaponEquipped();
}
