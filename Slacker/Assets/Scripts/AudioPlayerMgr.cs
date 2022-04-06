using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerMgr : MonoBehaviour
{
    public AudioSource undetected;
    public AudioSource detected;

    //save last knwon status
    private bool lastStatus;

    // Start is called before the first frame update
    void Start()
    {
        undetected.Play();
        lastStatus = BossAI.inst.followPath;
    }

    // Update is called once per frame
    void Update()
    {
        //checks if player is being followed and status has changed
        if (BossAI.inst.followPath && lastStatus != BossAI.inst.followPath)
        {
            detected.Play();
            undetected.Stop();
            lastStatus = !lastStatus;
        }else if(!BossAI.inst.followPath && lastStatus != BossAI.inst.followPath)
        {
            undetected.Play();
            detected.Stop();
            lastStatus = !lastStatus;
        }


       
    }
}
