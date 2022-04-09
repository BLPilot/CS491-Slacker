using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanRotate : MonoBehaviour
{
    public GameObject fanBlade;

    // Update is called once per frame
    void Update()
    {
        fanBlade.transform.Rotate(0f, .5f, 0f, Space.Self);
    }
}
