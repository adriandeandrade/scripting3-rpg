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
        [SerializeField] private PlayerCharacterStats characterStats;

        [Header("Panels")]
        [SerializeField] private SlotPanel inventoryPanel;
        [SerializeField] private SlotPanel craftingPanel;
        [SerializeField] private SlotPanel equipmentPanel;

        // Private Variables
        private Inventory inventory;
        private GameManager gameManager;
        private EquipmentManager equipmentManager;

        //Properties
        public PlayerCharacterStats CharacterStats => characterStats;
        public EquipmentManager EquipmentManager => equipmentManager;
        public Inventory Inventory => inventory;

        public SlotPanel InventoryPanel => inventoryPanel;
        public SlotPanel CraftingPanel => craftingPanel;
        public SlotPanel EquipmentPanel => equipmentPanel;

        public PlayerStats PlayerStats { get => playerStats; }

        protected override void Awake()
        {
            gameManager = GameManager.Instance;
            equipmentManager = GetComponent<EquipmentManager>();
            inventory = GetComponent<Inventory>();
            characterStats = GetComponent<PlayerCharacterStats>();
            weaponController = GetComponent<WeaponController>();
            playerStats = GetComponent<PlayerStats>();
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
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
