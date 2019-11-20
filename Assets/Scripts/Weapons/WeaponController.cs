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
    private EquipmentItem currentWeaponItem;

    // Properties

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Start()
    {
        weapon = null;
    }

    private void Update()
    {
        if(Input.GetKeyDown(attackKey))
        {
            PerformWeaponAttack();
        }
    }

    public void EquipWeapon(EquipmentItem weaponItem)
    {
        if (weaponItem == null) return;

        if(weapon != null && equippedWeapon != null)
        {
            UnEquipWeapon();
        }

        player.CharacterStats.AddModifiers(weaponItem);

        equippedWeapon = Instantiate(GameManager.Instance.ItemDatabase.GetSpawnablePrefab(weaponItem.fileName), hand);
        weapon = equippedWeapon.GetComponent<IWeapon>();
        weapon.OnWeaponEquipped();
        currentWeaponItem = weaponItem;
    }

    public void UnEquipWeapon()
    {
        if (equippedWeapon != null)
        {
            Destroy(equippedWeapon);
        }

        player.CharacterStats.RemoveModifiers(currentWeaponItem);
        weapon = null;
        currentWeaponItem = null;
        equippedWeapon = null;

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
