using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClosingTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //checks if player exits detection zone
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {

            Debug.Log("WINNER");
            SceneManager.LoadScene("Victory");

        }
    }

}
