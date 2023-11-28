using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scrolling : MonoBehaviour
{
    [SerializeField] RawImage _img;
    [SerializeField] private float _x, _y;
    public GameManager gm;
    int scoreThreshold = 50;
    public List<Texture> backgroundTextures;


    void Start()
    {
        float speedIncrease = GameManager.Instance.dataPersistence.shopItemsData[3, 1];
        _x += speedIncrease * 0.2f;
    }


    // Update is called once per frame
    void Update()
    {
        _img.uvRect = new Rect(_img.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _img.uvRect.size);

        if (gm.currentScore > scoreThreshold)
        {
            ChangeBackground();
        }
    }

    void ChangeBackground()
    {
        int backgroundIndex = Mathf.FloorToInt(gm.currentScore / scoreThreshold) % backgroundTextures.Count;
        _img.texture = backgroundTextures[backgroundIndex];
    }
}
