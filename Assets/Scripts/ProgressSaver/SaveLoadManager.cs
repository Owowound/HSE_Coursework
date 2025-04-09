using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private string filePath;

    public string FilePath { get { return filePath; } }

    [SerializeField]
    private PlayerData playerData;

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "FileData.dat");
    }

    public void SaveData()
    {
        Dictionary<StateManager.State, int> Levels = playerData.Levels;
        Dictionary< Location, bool> LocationsProgress = playerData.LocationsProgress;
        GameData data = new GameData(Levels, LocationsProgress, playerData.currentHP);

        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            formatter.Serialize(stream, data);
        }
    }

    public void LoadData()
    {
        GameData data = new GameData();
        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                data = formatter.Deserialize(stream) as GameData;
            }
        }
        else
        {
            Debug.Log("Файл не найден");
        }

        playerData.LoadData(data);
        GlobalEXP.needEXP = data.needEXP;
    }
}
