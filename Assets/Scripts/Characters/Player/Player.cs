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
            EquipmentManager = GetComponent<EquipmentManager>();
            Inventory = GetComponent<Inventory>();
            characterStats = GetComponent<PlayerCharacterStats>();
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
        }

        public bool PickupItem(string itemID)
        {
            Item item = GameManager.Instance.ItemDatabase.GetItem(itemID);

            Inventory.GiveItem(itemID);
            notificationManager.SpawnNotification(item);
            return true;
        }

        public void Save()
        {
            SlotPanelSaveData invPanel = SavePanel(inventoryPanel);
            SlotPanelSaveData equipPanel = SavePanel(equipmentPanel);

            PlayerStatSaveData playerStatsData = SavePlayerStats(playerStats);

            PlayerData saveObject = new PlayerData(invPanel, equipPanel, playerStatsData);

            string json = JsonUtility.ToJson(saveObject);
            SaveSystem.Save(json);
        }

        public void Load()
        {
            string saveString = SaveSystem.Load();

            if(saveString != null)
            {
                PlayerData saveObject = JsonUtility.FromJson<PlayerData>(saveString);
                LoadInventoryPanel(inventoryPanel, saveObject.inventoryPanel);
                LoadEquipmentPanel(equipmentPanel, saveObject.equipmentPanel);
                LoadPlayerStats(playerStats, saveObject.playerStats);
            }
        }

        private SlotPanelSaveData SavePanel(SlotPanel panel)
        {
            var saveData = new SlotPanelSaveData(panel.ItemSlots.Count);

            for (int i = 0; i < saveData.SavedSlots.Length; i++)
            {
                BaseItemSlot itemSlot = panel.ItemSlots[i];

                if (itemSlot.ItemInSlot == null)
                {
                    saveData.SavedSlots[i] = null;
                }
                else
                {
                    saveData.SavedSlots[i] = new BaseItemSlotData(itemSlot.ItemInSlot.id);
                }
            }

            return saveData;
        }

        private void LoadInventoryPanel(SlotPanel panel, SlotPanelSaveData panelData)
        {
            if (panelData == null) return;

            panel.EmptyAllSlots();
            Inventory.ClearItems();

            for (int i = 0; i < panelData.SavedSlots.Length; i++)
            {
                BaseItemSlot itemSlot = panel.ItemSlots[i];
                BaseItemSlotData savedSlot = panelData.SavedSlots[i];

                if (savedSlot.itemID == 0)
                {
                    itemSlot.ItemInSlot = null;
                }
                else
                {
                    itemSlot.ItemInSlot = GameManager.Instance.ItemDatabase.GetItem(savedSlot.itemID);
                    itemSlot.UpdateSlot(itemSlot.ItemInSlot);
                    Inventory.Items.Add(itemSlot.ItemInSlot);
                }
            }
        }

        private void LoadEquipmentPanel(SlotPanel panel, SlotPanelSaveData panelData)
        {
            if (panelData == null) return;

            panel.EmptyAllSlots();
            GameManager.Instance.PlayerRef.EquipmentManager.UnequipAll();

            foreach (BaseItemSlotData savedSlot in panelData.SavedSlots)
            {
                if (savedSlot == null)
                    continue;

                Item item = GameManager.Instance.ItemDatabase.GetItem(savedSlot.itemID);
                GameManager.Instance.PlayerRef.EquipmentManager.Equip(item as EquipmentItem);
            }

            //Debug.Log($"Equipment Loaded");
        }

        private PlayerStatSaveData SavePlayerStats(PlayerStats _playerStats)
        {
            var saveData = new PlayerStatSaveData(_playerStats.CurrentLevel, _playerStats.CurrentXP);

            saveData.currentLevel = _playerStats.CurrentLevel;
            saveData.currentXP = _playerStats.CurrentXP;

            return saveData;
        }

        private void LoadPlayerStats(PlayerStats _playerStats, PlayerStatSaveData playerStatsData)
        {
            _playerStats.Load(playerStatsData);
        }
    }

    public class PlayerData
    {
        public SlotPanelSaveData inventoryPanel;
        public SlotPanelSaveData equipmentPanel;
        public PlayerStatSaveData playerStats;

        public PlayerData(SlotPanelSaveData inventoryPanel, SlotPanelSaveData equipmentPanel, PlayerStatSaveData playerStats)
        {
            this.inventoryPanel = inventoryPanel;
            this.equipmentPanel = equipmentPanel;
            this.playerStats = playerStats;
        }
    }
}
