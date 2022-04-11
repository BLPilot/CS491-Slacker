using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    // Timer variables
    public float timer = 180;
    public int gameMinutes = 0;
    public int gameSeconds = 0;
    public TextMeshProUGUI stopwatch;

    public TextMeshProUGUI objective;

    public BossAI boss;
    public bool updatingSpeed = false;

    public GameOverCollision gameOverCheck;
    public GameObject winnerPOV;

    public GameObject exitDoorBox;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
        gameMinutes = Mathf.FloorToInt(timer / 60f);
        gameSeconds = Mathf.FloorToInt(timer % 60f);
        if (gameMinutes >= 0 && gameSeconds >= 0)
        {
            stopwatch.text = gameMinutes.ToString("00") + ":" + gameSeconds.ToString("00");
        }

        BossGoBrrr();

        if (gameMinutes == 0 && gameSeconds == 0)
        {
            //gameOverCheck.gameOver = true;
            //winnerPOV.SetActive(true);
            objective.text = "The day is over! Get out the building!";
            ClosingTime();
        }
    }

    // Increase boss speed at 2 minutes and at 1 minute
    void BossGoBrrr()
    {
        if (gameMinutes == 2 && gameSeconds == 0)
        {

            boss.speed = 7.3f;
        }
        else if (gameMinutes == 1 && gameSeconds == 0)
        {
            boss.speed = 7.65f;
        }
    }

    // enable the exit!
    void ClosingTime()
    {
        exitDoorBox.SetActive(true);
    }
}
