using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using enjoii.Items;

namespace enjoii.Characters
{
    public class Player : BaseCharacter
    {
        //Inspector Fields
        [Header("Character Configuration")]
        [SerializeField] private NotificationManager notificationManager;
        [SerializeField] private WeaponController weaponController;
        [SerializeField] private Transform weaponSlot;
        [SerializeField] private GameObject bowPrefab;

        [Header("Stats")]
        [SerializeField] private PlayerStats playerStats;
        [SerializeField] private CharacterStats characterStats;

        // Private Variables
        private Inventory inventory;
        private GameManager gameManager;
        private EquipmentManager equipmentManager;

        //Properties
        public CharacterStats CharacterStats => characterStats;
        public Inventory Inventory => inventory;
        public EquipmentManager EquipmentManager => equipmentManager;

        protected override void Awake()
        {
            gameManager = GameManager.Instance;
            inventory = GetComponent<Inventory>();
            equipmentManager = GetComponent<EquipmentManager>();
            weaponController = GetComponent<WeaponController>();
            characterStats = new CharacterStats(5, 5);
            base.Awake();
        }

        public void OnXPAdded(float amount)
        {
            playerStats.AddXP(amount);
            Debug.Log($"{amount} of XP was gained.");
        }

        public bool PickupItem(string itemID)
        {
            Item item = GameManager.Instance.ItemDatabase.GetItem(itemID);

            inventory.GiveItem(itemID);
            notificationManager.SpawnNotification(item);
            return true;
        }
    }
}
