using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using enjoii.Items;
using enjoii.Items.Slot;
using System.IO;

namespace enjoii.Characters
{
    public class Player : BaseCharacter
    {
        //Inspector Fields
        [Header("Character Configuration")]
        [SerializeField] private NotificationManager notificationManager;

        [Header("Stats")]
        [SerializeField] private PlayerStats playerStats;
        [SerializeField] private PlayerCharacterStats characterStats;
        [SerializeField] private PlayerSaveManager playerSaveManager;

        [Header("Panels")]
        [SerializeField] private SlotPanel inventoryPanel;
        [SerializeField] private SlotPanel craftingPanel;
        [SerializeField] private SlotPanel equipmentPanel;

        //Properties
        public PlayerCharacterStats CharacterStats => characterStats;
        public EquipmentManager EquipmentManager { get; set; }
        public Inventory Inventory { get; private set; }

        public SlotPanel InventoryPanel => inventoryPanel;
        public SlotPanel CraftingPanel => craftingPanel;
        public SlotPanel EquipmentPanel => equipmentPanel;

        public PlayerStats PlayerStats { get => playerStats; }

        protected override void Awake()
        {
            playerSaveManager = GetComponent<PlayerSaveManager>();
            EquipmentManager = GetComponent<EquipmentManager>();
            Inventory = GetComponent<Inventory>();
            characterStats = GetComponent<PlayerCharacterStats>();
            playerStats = GetComponent<PlayerStats>();
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
            characterStats.UpdateStatText();
        }

        public void OnXPAdded(float amount)
        {
            playerStats.AddXP(amount);
        }

        public bool PickupItem(string itemID)
        {
            Item item = GameManager.Instance.ItemDatabase.GetItem(itemID);

            Inventory.GiveItem(itemID);
            notificationManager.SpawnNotification(item);
            return true;
        }

        public void HandleConsumable(ConsumableItem consumable)
        {
            Debug.Log($"Player used {consumable.name}");
            IncreaseHealth(consumable.benefitAmount);
        }

        public void Save()
        {
            SlotPanelData invPanel = playerSaveManager.SavePanel(inventoryPanel);
            SlotPanelData equipPanel = playerSaveManager.SavePanel(equipmentPanel);

            PlayerStatData playerStatsData = playerSaveManager.SavePlayerStats(playerStats);

            TransformData transformData = playerSaveManager.SavePlayerTransform(transform);

            PlayerData saveObject = new PlayerData(invPanel, equipPanel, playerStatsData, transformData);

            FileReadWrite.WriteToJsonFile(saveObject);
        }

        public void Load()
        {
            PlayerData saveObject = FileReadWrite.ReadFromJsonFile<PlayerData>();

            if(saveObject != null)
            {
                playerSaveManager.LoadInventoryPanel(Inventory, inventoryPanel, saveObject.inventoryPanel);
                playerSaveManager.LoadEquipmentPanel(equipmentPanel, saveObject.equipmentPanel);
                playerSaveManager.LoadPlayerStats(playerStats, saveObject.playerStats);
                playerSaveManager.LoadPlayerTransform(transform, saveObject.transformData);
            }
        }
    }
}
