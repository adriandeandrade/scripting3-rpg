using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using enjoii.Items;

public interface IWeapon
{
    int CurrentDamage { get; set; }
    int ItemID { get; set; }
    void PerformAttack(int damage);
    void PerformSpecialAttack();
    void OnWeaponEquipped();
}
