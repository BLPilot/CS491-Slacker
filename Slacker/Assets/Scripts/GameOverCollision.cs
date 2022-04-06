using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCollision : MonoBehaviour
{
    public bool gameOver = false;

    public GameObject gameOverScreen;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player" && !gameOver)
        {
            gameOver = true;
            gameOverScreen.SetActive(gameOver);
            Debug.Log("Collided GameOver");
        }

    }
}
