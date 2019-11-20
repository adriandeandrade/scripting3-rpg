using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using enjoii.Items;
using enjoii.Characters;

public class WeaponController : MonoBehaviour
{
    // Inspector Fields
    [Header("Weapon Controller Configuration")]
    [SerializeField] private Transform hand;
    [SerializeField] private GameObject equippedWeapon;
    [SerializeField] private KeyCode attackKey = KeyCode.Space;

    // Private Variables
    private Player player;
    private IWeapon weapon;

    // Properties

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(attackKey))
        {
            PerformWeaponAttack();
        }
    }

    //private void EquipWeapon(EquippableItem item)
    //{
    //    if(equippedWeapon != null)
    //    {
    //        UnEquipWeapon();
    //    }

    //    equippedWeapon = Instantiate(item.GetSpawnablePrefab(), hand);
    //    weapon = equippedWeapon.GetComponent<IWeapon>();
    //    weapon.OnWeaponEquipped();

    //    player.CharacterStats.AddStatModifier(item.Stats);

    //    Debug.Log($"Item: {item.ItemName} was equipped.");
    //}

    private void UnEquipWeapon()
    {
        if (weapon == null) weapon = equippedWeapon.GetComponent<IWeapon>();

        // TODO: Remove stat modifiers from character stats.
        Destroy(equippedWeapon);
        weapon = null;

        Debug.Log($"Item: was un equipped.");
    }

    public void PerformWeaponAttack()
    {
        IWeapon weapon = equippedWeapon.GetComponent<IWeapon>();

        if(weapon != null)
        {
            // TODO: Change this so damage is based on stats instead.
            weapon.PerformAttack(1);
            Debug.Log("Attack performed.");
        }
    }
}
