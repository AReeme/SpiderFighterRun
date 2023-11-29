using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonInfoOptions : MonoBehaviour
{

    public int ItemID;
    public TMPro.TMP_Text Price;
    public TMPro.TMP_Text Quantity;
    public GameObject ShopManager;

    // Update is called once per frame
    void Update()
    {
        try
        {
            Price.text = "Price: " + ShopManager.GetComponent<ShopManagerScript>().dataPersistence.shopItemsData[2, ItemID].ToString();
            Quantity.text = ShopManager.GetComponent<ShopManagerScript>().dataPersistence.shopItemsData[3, ItemID].ToString();
        }
        catch (System.Exception ex)
        {
            // Log the error for debugging purposes
            Debug.Log("Error occurred in ShopManager: Do not load shop by itself");
            // Load Scene 2
            SceneManager.LoadScene("Scene 2");
        }
    }
}