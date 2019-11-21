using UnityEngine;
using System.IO;

public class PlayerStatsSaveIO
{
    private static readonly string baseSavePath;

    static PlayerStatsSaveIO()
    {
        baseSavePath = Application.persistentDataPath;
    }

    public static void SaveItems(PlayerStatSaveData items, string path)
    {
        FileReadWrite.WriteToBinaryFile($"{baseSavePath}/{path}.dat", items);
    }

    public static PlayerStatSaveData LoadItems(string path)
    {
        string filePath = $"{baseSavePath}/{path}.dat";

        if (File.Exists(filePath))
        {
            return FileReadWrite.ReadFromBinaryFile<PlayerStatSaveData>(filePath);
        }

        return null;
    }
}
