using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SavedPlayerData
{
    public static void SaveData(PlayerData playerData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string Path = Application.persistentDataPath + "/Player.SavedData";
        FileStream stream=new FileStream(Path, FileMode.Create);

        formatter.Serialize(stream, playerData);

        stream.Close();
    }
    public static PlayerData LoadData()
    {
        string Path = Application.persistentDataPath + "/Player.SavedData";
        if (File.Exists(Path))
        {
            BinaryFormatter formatter=new BinaryFormatter();
            FileStream stream=new FileStream(Path,FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close();

            return data;
        }
        else
        {
            return null;
        }

    }
    
}
