using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManagerScript : MonoBehaviour
{
    public int[,] shopItems = new int[5, 5];
    public float points;
    public TMPro.TextMeshPro Coins;

    // Start is called before the first frame update
    void Start()
    {
        Coins.text = "Points: " + points.ToString();

        shopItems[1, 1] = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
