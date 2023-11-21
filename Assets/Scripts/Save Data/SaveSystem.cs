using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public static class SaveSystem
{
    public static readonly string SAVE_FOLDER = Application.persistentDataPath + "/saves/";
    public static readonly string FILE_EXT = ".json";

    // Generic method to save any type of data
    public static void Save(string filename, Data dataToSave)
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }

        string jsonData = JsonConvert.SerializeObject(dataToSave, Formatting.Indented);
        File.WriteAllText(SAVE_FOLDER + filename + FILE_EXT, jsonData);
    }

    // Generic method to load any type of data
    public static T Load<T>(string filename)
    {
        string fileLoc = SAVE_FOLDER + filename + FILE_EXT;
        if (File.Exists(fileLoc))
        {
            string loadedData = File.ReadAllText(fileLoc);
            return JsonConvert.DeserializeObject<T>(loadedData); // Deserialize JSON to object
        }
        else
        {
            return default(T);
        }
    }

    public static void DeleteSave(string filename)
    {
        string fileLoc = SAVE_FOLDER + filename + FILE_EXT;
        if (File.Exists(fileLoc))
        {
            File.Delete(fileLoc);
        }
    }
}
