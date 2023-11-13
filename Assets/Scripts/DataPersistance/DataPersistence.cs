using UnityEngine;

public class DataPersistence : MonoBehaviour
{
    public static DataPersistence Instance { get; private set; } // Singleton pattern

    public int[,] shopItemsData = new int[5, 5];
    public float points; // Add a variable to store points

    private void Awake()
    {
        // Check if an instance already exists
        if (Instance == null)
        {
            Instance = this; // Set the instance to this object
            DontDestroyOnLoad(gameObject); // Make sure this object is not destroyed when loading scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }
}
