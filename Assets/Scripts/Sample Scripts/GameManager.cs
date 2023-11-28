using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton
        
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    #endregion

    public int lives;
    public float speed;

    public float currentScore = 0f;
    public float coinsEarned;
    public int conversionRate = 10;

    public Data data;

    public bool isPlaying = false;

    public UnityEvent onPLay = new UnityEvent();
    public UnityEvent onGameOver = new UnityEvent();
    public DataPersistence dataPersistence;
    public GameObject Jump;
    public GameObject Crouch;

    public GameObject Player;
    public GameObject Ground;

    [SerializeField] private AudioSource deathAudio;
    [SerializeField] private AudioSource startScreenAudio;
    [SerializeField] private AudioSource gameAudio;
    [SerializeField] private AudioSource gameOverAudio;

    private void Start()
    {
        startScreenAudio.Play();
        dataPersistence = DataPersistence.Instance;
        Data loadedDataObj = SaveSystem.Load<Data>("Save");
        if (loadedDataObj != null)
        {
            data = loadedDataObj;
        }
        else
        {
            // Initialize data with default values if nothing is loaded
            data = new Data();
            InitializeShopItems();
        }
        lives = dataPersistence.shopItemsData[3, 2];
    }

    private void Update()
    {
        if (isPlaying)
        {
            currentScore += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Tab) && isPlaying)
        {
            currentScore += 2;
        }
    }

    public void StartGame()
    {
        gameOverAudio.Stop();
        onPLay.Invoke();
        isPlaying = true;
        currentScore = 0f;
        Jump.SetActive(true);
        Crouch.SetActive(true);
        Player.SetActive(true);
        Ground.SetActive(true);
        startScreenAudio.Stop();
        gameAudio.Play();
    }

    public void GameOver()
    {
        gameAudio.Stop();
        PointsToCoins(currentScore);
        dataPersistence.points += Mathf.RoundToInt(coinsEarned);
        dataPersistence.LoadData();

        if (data.highScore < currentScore)
        {
            data.highScore = currentScore;
            string saveString = JsonUtility.ToJson(data);
        }
        deathAudio.Play();
        isPlaying = false;
        dataPersistence.LoadData(UpdateGameOverUI);
        Jump.SetActive(false);
        Crouch.SetActive(false);
        Player.SetActive(false);
        Ground.SetActive(false);
        gameOverAudio.Play();
    }

    private void UpdateGameOverUI()
    {
        onGameOver.Invoke();
    }

    public string PrettyScore()
    {
        return Mathf.RoundToInt(currentScore).ToString();
    }

    public string PrettyHighscore()
    {
        return Mathf.RoundToInt(data.highScore).ToString();
    }

    public void Shop()
    {
        SceneManager.LoadScene("Shop");
    }

    public int PointsToCoins(float points)
    {
        coinsEarned = points / conversionRate;
        return Mathf.RoundToInt(coinsEarned);
    }

    private void InitializeShopItems()
    {

        Debug.Log("Shop Reinitilized");
        // Ensure that the dataPersistence.shopItemsData array is initialized
        dataPersistence.shopItemsData = new int[4, 6];

        // ID's
        for (int i = 1; i <= 5; i++)
        {
            dataPersistence.shopItemsData[1, i] = i;
        }

        // Prices
        dataPersistence.shopItemsData[2, 1] = 5;
        dataPersistence.shopItemsData[2, 2] = 10;
        dataPersistence.shopItemsData[2, 3] = 30;
        dataPersistence.shopItemsData[2, 4] = 40;
        dataPersistence.shopItemsData[2, 5] = 100;

        // Quantities
        for (int i = 1; i <= 5; i++)
        {
            dataPersistence.shopItemsData[3, i] = 0;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
