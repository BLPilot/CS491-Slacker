using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public static BossAI inst;

    //boss character controller
    public CharacterController controller;

    public Animator animations;

    //boss game object
    public GameObject boss;

    //boss parameters
    public float speed = 4f;
    public float turnSpeed = 180f;

    //if ai should follow path
    public bool followPath = false;

    //if collision it is true
    public bool detectionRangeCollision = false;

    //final path of pathfinding
    public List<NodeMgr> finalPath;//final path of a star

    //saved path
    public List<NodeMgr> savedPath;

    //keep track of current node being moved to
    public int currentNodeIndex = 0;

    //reached location
    bool reached = true;

    public void Awake()
    {
        inst = this;
        
    }

    public void Update()
    {
        
        if (followPath && currentNodeIndex < finalPath.Count)
        {
            MoveAlongPath(finalPath);

        }
    }

    //move boss along path
    public void MoveAlongPath(List<NodeMgr> path)
    {
        if(Vector3.Distance(finalPath[currentNodeIndex].nodePos, boss.transform.position) < 10)
        {
            reached = true;
        }
        //checks if it is within certain distance of next node before moving to next node


        if (reached) {
            float x = finalPath[currentNodeIndex].nodePos.x;
            float y = finalPath[currentNodeIndex].nodePos.z;

            Vector3 currentNodePos = new Vector3(finalPath[currentNodeIndex].nodePos.x, 0, finalPath[currentNodeIndex].nodePos.z);

            Vector3 move = new Vector3(finalPath[currentNodeIndex].nodePos.x - boss.transform.position.x, 0, finalPath[currentNodeIndex].nodePos.z - boss.transform.position.z);//new Vector3(x,1, y);

            //move.Normalize();

            //controller.Move(move * speed * Time.deltaTime);

            boss.transform.position = Vector3.MoveTowards(boss.transform.position, currentNodePos, speed * Time.deltaTime);

            Debug.Log(finalPath[currentNodeIndex].nodePos);
            Debug.Log("Moving to:" + move);

            //face direction
            if (move != Vector3.zero)
            {
                transform.forward = move;

                animations.SetBool("Walking", true);

                Quaternion toRotate = Quaternion.LookRotation(move, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, turnSpeed * Time.deltaTime);
            }
            else { animations.SetBool("Walking", false); }

            //currentNodeIndex++;
            reached = false;

        }
    }

    //checks if player enter detection zone
    private void OnTriggerStay(Collider collision)
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
