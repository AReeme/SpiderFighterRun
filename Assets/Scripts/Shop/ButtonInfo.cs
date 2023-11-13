using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonInfo : MonoBehaviour
{

    public int ItemID;
    public TMPro.TMP_Text Price;
    public TMPro.TMP_Text Quantity;
    public GameObject ShopManager;

    // Update is called once per frame
    void Update()
    {
        Price.text = "Price: $" + ShopManager.GetComponent<ShopManagerScript>().shopItems[2, ItemID].ToString();
        Quantity.text = ShopManager.GetComponent<ShopManagerScript>().shopItems[3, ItemID].ToString();
    }
}
