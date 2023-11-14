using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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

    public Data data;

    public bool isPlaying = false;

    public UnityEvent onPLay = new UnityEvent();
    public UnityEvent onGameOver = new UnityEvent();

    private void Start()
    {
        string loadedData = SaveSystem.Load("Save");
        if (loadedData != null) 
        { 
            data = JsonUtility.FromJson<Data>(loadedData);
        } else
        {
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
        if (data.highScore < currentScore) 
        {
            data.highScore = currentScore;
            string saveString = JsonUtility.ToJson(data);
            SaveSystem.Save("Save", saveString);
        }
        isPlaying = false;
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
}
