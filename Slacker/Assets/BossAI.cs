using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    //if collision it is true
    public bool detectionRangeCollision = false;



    private void OnTriggerEnter(Collision collision)
    {
        detectionRangeCollision = true;
        Debug.Log("Collided");
    }


}
