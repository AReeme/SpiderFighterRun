using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public bool isPlaying = false;

    public UnityEvent onPLay = new UnityEvent();

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
    }

    public void GameOver()
    {
        currentScore = 0f;
        isPlaying = false;
    }

    public string PrettyScore()
    {
        return Mathf.RoundToInt(currentScore).ToString();
    }
}