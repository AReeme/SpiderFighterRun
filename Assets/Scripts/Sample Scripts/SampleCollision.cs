using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleCollision : MonoBehaviour
{
    public GameManager gm;
    public int Lives;
    private void Start()
    {
        Lives = GameManager.Instance.lives;
        if (Lives == 1)
        {
            Lives = 0;
        }
        Debug.Log("Lives set to:" + Lives);
        GameManager.Instance.onPLay.AddListener(ActivatePlayer);
    }

    private void ActivatePlayer()
    {
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Obstacle")
        {
            if (Lives <= 0)
            {
                gameObject.SetActive(false);
                GameManager.Instance.GameOver();
            }
            else
            {
                other.gameObject.SetActive(false);
                Lives--;
            }
        }
    }
}
