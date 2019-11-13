using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using enjoii.Items;
public class Weapon : MonoBehaviour
{
    // Inspector Fields
    [Header("Weapon Configuration")]
    [SerializeField] protected WeaponTypes weaponType;

    // Private Variables
    protected EquippableItem weapon;

    public void SetWeapon(EquippableItem _weapon)
    {
        weapon = _weapon;
    }

    public void DestroyWeapon()
    {
        Destroy(gameObject);
        Debug.Log("weapon Destroyed");
    }
}
