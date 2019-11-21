using UnityEngine;
using System.IO;

public static class ItemSaveIO
{
    private static readonly string baseSavePath;

    static ItemSaveIO()
    {
        baseSavePath = Application.persistentDataPath;
    }

    public static void SaveItems(SlotPanelSaveData items, string path)
    {
        FileReadWrite.WriteToBinaryFile($"{baseSavePath}/{path}.dat", items);
    }

    public static SlotPanelSaveData LoadItems(string path)
    {
        string filePath = $"{baseSavePath}/{path}.dat";

        if (File.Exists(filePath))
        {
            return FileReadWrite.ReadFromBinaryFile<SlotPanelSaveData>(filePath);
        }

        return null;
    }
}
