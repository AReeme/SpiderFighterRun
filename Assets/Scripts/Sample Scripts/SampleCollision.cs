using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleCollision : MonoBehaviour
{
    public GameManager gm;
    public int Lives = 5;
    private void Start()
    {

        GameManager.Instance.onPLay.AddListener(ActivatePlayer);
    }

    private void ActivatePlayer()
    {
        gameObject.SetActive(true);
        Lives = GameManager.Instance.lives;
        Debug.Log("Lives set to:" + Lives);
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
