using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    private static readonly string SavePath = Application.persistentDataPath + "/player_save.wally";
    private static GameSave _gameSave;
    public static GameSave SavedGame => _gameSave;

    public static void NewSave(string playerName)
    {
        _gameSave = new GameSave(playerName);
        SaveData();
    }
    
    public static void SaveData()
    {
        using var stream = new FileStream(SavePath, FileMode.OpenOrCreate, FileAccess.Write);
        var formatter = new BinaryFormatter();
        formatter.Serialize(stream, _gameSave);
    }

    public static void LoadData()
    {
        using var stream = new FileStream(SavePath, FileMode.OpenOrCreate, FileAccess.Read);
        var formatter = new BinaryFormatter();
        _gameSave = formatter.Deserialize(stream) as GameSave;
    }
}