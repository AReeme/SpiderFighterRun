using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private GameObject startMenuUI;
    GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
    }

    public void PlayButtonHandler()
    {
        gm.StartGame();
        startMenuUI.SetActive(false);
    }

    private void OnGUI()
    {
        scoreUI.text = gm.PrettyScore();
    }
}
