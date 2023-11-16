using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ShopManagerScript : MonoBehaviour
{
    private float points;
    public TMP_Text Coins;

    public DataPersistence dataPersistence;
    public Data data;

    public AudioSource poorBitch;

    private void Start()
    {
        dataPersistence = DataPersistence.Instance; // Get reference to the DataPersistence script

        // Initialize shop items (if not already initialized)
        if (dataPersistence.shopItemsData[1, 1] == 0)
        {
            // ID's
            dataPersistence.shopItemsData[1, 1] = 1;
            dataPersistence.shopItemsData[1, 2] = 2;
            dataPersistence.shopItemsData[1, 3] = 3;
            dataPersistence.shopItemsData[1, 4] = 4;
            dataPersistence.shopItemsData[1, 5] = 4;
            // Price's
            dataPersistence.shopItemsData[2, 1] = 1000;
            dataPersistence.shopItemsData[2, 2] = 2000;
            dataPersistence.shopItemsData[2, 3] = 3000;
            dataPersistence.shopItemsData[2, 4] = 4000;
            dataPersistence.shopItemsData[2, 5] = 10000;
            // Quantity's
            dataPersistence.shopItemsData[3, 1] = 0;
            dataPersistence.shopItemsData[3, 2] = 0;
            dataPersistence.shopItemsData[3, 3] = 0;
            dataPersistence.shopItemsData[3, 4] = 0;
            dataPersistence.shopItemsData[3, 5] = 0;
        }

        // Set the initial points from DataPersistence
        points = dataPersistence.points;
        UpdatePointsText();
    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (points >= dataPersistence.shopItemsData[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            points -= dataPersistence.shopItemsData[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];

            dataPersistence.shopItemsData[3, ButtonRef.GetComponent<ButtonInfo>().ItemID]++;

            // Update points in DataPersistence
            dataPersistence.points = points;

            UpdatePointsText();

            ButtonRef.GetComponent<ButtonInfo>().Quantity.text = dataPersistence.shopItemsData[3, ButtonRef.GetComponent<ButtonInfo>().ItemID].ToString();
        }
        else
        {
            int pitch = Random.RandomRange(5, 10);

            poorBitch.pitch = pitch;
            poorBitch.Play();
        }

    }

    private void UpdatePointsText()
    {
        dataPersistence.points = points;
        Coins.text = "Points: " + points.ToString();
    }

    public void Return()
    {
        dataPersistence.LoadData();
        SceneManager.LoadScene("Scene 2");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            points += 1000;
            UpdatePointsText();
        }
    }


}
