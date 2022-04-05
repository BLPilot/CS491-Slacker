using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCollision : MonoBehaviour
{
    bool gameOver = false;

    public GameObject gameOverScreen;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            gameOver = true;
            gameOverScreen.SetActive(gameOver);
            Debug.Log("Collided GameOver");
        }

    }
}
