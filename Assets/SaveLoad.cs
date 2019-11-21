using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveLoad
{
    public static void Save()
    {
        GameManager.Instance.SaveManager.SaveInventory(GameManager.Instance.PlayerRef.InventoryPanel);
        GameManager.Instance.SaveManager.SaveEquipment(GameManager.Instance.PlayerRef.EquipmentPanel);
        GameManager.Instance.SaveManager.SaveStats(GameManager.Instance.PlayerRef.PlayerStats);
    }

    public static void Load()
    {
        GameManager.Instance.SaveManager.LoadInventory(GameManager.Instance.PlayerRef.InventoryPanel);
        GameManager.Instance.SaveManager.LoadEquipment(GameManager.Instance.PlayerRef.EquipmentPanel);
        GameManager.Instance.SaveManager.LoadStats(GameManager.Instance.PlayerRef.PlayerStats);
    }
}
