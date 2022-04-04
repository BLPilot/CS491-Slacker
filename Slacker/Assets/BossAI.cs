using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public static BossAI inst;

    public void Awake()
    {
        inst = this;
    }

    //if collision it is true
    public bool detectionRangeCollision = false;




    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.name == "Player")
        {
            detectionRangeCollision = true;
            Debug.Log("Collided");
        }
        
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.name == "Player")
        {
            StartCoroutine(DelayCollisionExit());
            
        }
    }

    IEnumerator DelayCollisionExit()
    {
        yield return new WaitForSeconds(5);
        detectionRangeCollision = false;
    }

}
