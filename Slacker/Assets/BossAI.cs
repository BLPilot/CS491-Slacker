using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public static BossAI inst;

    //boss character controller
    private CharacterController controller;

    public Animator animations;

    //boss game object
    public GameObject boss;

    //boss parameters
    public float speed = 4f;
    public float turnSpeed = 180f;

    //if ai should follow path
    private bool followPath = false;

    //if collision it is true
    public bool detectionRangeCollision = false;

    //final path of pathfinding
    public List<NodeMgr> finalPath;//final path of a star

    //keep track of current node being moved to
    public int currentNodeIndex = 0;

    public void Awake()
    {
        inst = this;
        
    }

    public void Update()
    {
        /*
        if (followPath && currentNodeIndex < finalPath.Count)
        {
            MoveAlongPath(finalPath);
        }*/
    }

    //move boss along path
    public void MoveAlongPath(List<NodeMgr> path)
    {
        //checks if it is within certain distance of next node before moving to next node
        if (Vector3.Distance(boss.transform.position, finalPath[currentNodeIndex].nodePos) > 1) {
            float x = finalPath[currentNodeIndex].nodePos.x;
            float y = finalPath[currentNodeIndex].nodePos.z;

            Vector3 move = new Vector3(x,0, y);
            move.Normalize();

            //face direction
            if (move != Vector3.zero)
            {
                transform.forward = move;

                animations.SetBool("Walking", true);

                Quaternion toRotate = Quaternion.LookRotation(move, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, turnSpeed * Time.deltaTime);
            }
            else { animations.SetBool("Walking", false); }

            currentNodeIndex++;

        }
    }

    //checks if player enter detection zone
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.name == "Player")
        {
            detectionRangeCollision = true;
            followPath = true;
            Debug.Log("Collided");
        }
        
    }

    //checks if player exits detection zone
    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.name == "Player")
        {
            
            StartCoroutine(DelayCollisionExit());
            
        }
    }

    //waits five seconds before forgeting player location
    IEnumerator DelayCollisionExit()
    {
        yield return new WaitForSeconds(5);
        detectionRangeCollision = false;
        followPath = false;
    }

}
