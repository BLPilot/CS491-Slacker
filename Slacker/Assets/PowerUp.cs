using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    //parameters
    public float spawnRate;
    public float speedIncrease;
    public float activeTime;

    //player reference
    public PlayerController player;

    //boss reference
    public BossAI boss;

    //reference to gameobject
    public GameObject coffee;

    //rotate speed
    public float rotateSpeed;

    //save old speed
    float oldSpeed;

    public AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        oldSpeed = player.speed;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCoffee();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {

            coffee.SetActive(false);
            player.speed = boss.speed + speedIncrease;
            sound.Play();
            StartCoroutine(DelayPowerRespawn());
            StartCoroutine(DelayActiveTimeDisable());

           
        }
    }

    IEnumerator DelayPowerRespawn()
    {
        yield return new WaitForSeconds(spawnRate);
        coffee.SetActive(true);
    }

    IEnumerator DelayActiveTimeDisable()
    {
        yield return new WaitForSeconds(activeTime);
        player.speed = oldSpeed;
    }


    private void RotateCoffee()
    {
        coffee.transform.Rotate(0.0f, rotateSpeed * Time.deltaTime, 0.0f, Space.World);
    }
}
