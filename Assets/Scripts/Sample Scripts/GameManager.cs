using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class GameManager : MonoBehaviour
{
    #region Singleton
        
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    #endregion

    public float currentScore = 0f;
    public float coinsEarned;
    public int conversionRate = 10;

    public Data data;

    public bool isPlaying = false;

    public UnityEvent onPLay = new UnityEvent();
    public UnityEvent onGameOver = new UnityEvent();
    public DataPersistence dataPersistence;

    [SerializeField] private AudioSource deathAudio;

    private void Start()
    {
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
        }
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
        onPLay.Invoke();
        isPlaying = true;
        currentScore = 0f;
    }

    public void GameOver()
    {
        dataPersistence.LoadData();

        if (data.highScore < currentScore)
        {
            data.highScore = currentScore;
            string saveString = JsonUtility.ToJson(data);
        }
        deathAudio.Play();
        isPlaying = false;
        PointsToCoins(currentScore);
        dataPersistence.LoadData(UpdateGameOverUI);
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

}
