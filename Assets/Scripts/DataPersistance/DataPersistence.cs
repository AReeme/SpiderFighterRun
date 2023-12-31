using UnityEngine;
using Newtonsoft.Json;
using System;

public class DataPersistence : MonoBehaviour
{
    public static DataPersistence Instance { get; private set; } // Singleton pattern

    public int[,] shopItemsData = new int[4, 6];
    public float points;
    public float highScore;
    public Data data;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Load the data and check for null
        Data loadedDataObj = SaveSystem.Load<Data>("Save");
        if (loadedDataObj != null)
        {
            data = loadedDataObj;
            shopItemsData = loadedDataObj.shopItemsData;
            points = loadedDataObj.points;
        }
        else
        {
            // Initialize data with default values if nothing is loaded
            data = new Data();
        }
    }

    public void LoadData(Action onSavedCallback = null)
    {

        if (data.highScore < GameManager.Instance.currentScore)
        {
            data.highScore = GameManager.Instance.currentScore;
        }
            data.shopItemsData = shopItemsData;
            string saveString = JsonUtility.ToJson(data);
            SaveSystem.Save("Save", data);
            onSavedCallback?.Invoke();
    }

    public void SaveData()
    {
        data.shopItemsData = shopItemsData;
        data.points = points;
        data.highScore = highScore;
        string saveString = JsonUtility.ToJson(data);
        SaveSystem.Save("Save", data);
    }

    public void DeleteData()
    {
        SaveSystem.DeleteSave("Save");
        Debug.Log("Save File Deleted");
    }
}
