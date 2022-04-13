using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoWorkers : MonoBehaviour
{
    public static CoWorkers inst;

    //boss character controller
    public CharacterController controller;

    public Animator animations;

    //coworker game object
    public GameObject CoWorker;

    //reference to boss
    public BossAI boss;

    public List<GameObject> roomWaypoints;

    //boss parameters
    public float speed = 3f;
    public float turnSpeed = 180f;

    //if ai should follow path
    private bool followPath = false;

    //if collision it is true
    //public bool detectionRangeCollision = false;

    //final path of pathfinding
    public List<NodeMgr> finalPath;//final path of a star

    //saved path
    public List<NodeMgr> savedPath;

    public PathfindingMgr pgr;

    //keep track of current node being moved to
    public int currentNodeIndex = 0;

    //reached next node location
    bool reached = true;

    //random waypoint to reach
    int randomPoint;

    //is a snitcher
    public bool isSnitch;



    public void Awake()
    {
        inst = this;

    }

    public void Start()
    {
        finalPath = new List<NodeMgr>();
        randomPoint = Random.Range(0, roomWaypoints.Count - 1);

        followPath = false;

    }

    public void Update()
    {
      

        ReachedWaypoint();
        GetWaypoint();

        if (followPath && currentNodeIndex < finalPath.Count)
        {
            MoveAlongPath(finalPath);

        }

        followPath = true;


    }

    //move boss along path
    public void MoveAlongPath(List<NodeMgr> path)
    {
        if (Vector3.Distance(finalPath[currentNodeIndex].nodePos, CoWorker.transform.position) < 10)
        {
            reached = true;
        }
        //checks if it is within certain distance of next node before moving to next node


        if (reached)
        {
            float x = finalPath[currentNodeIndex].nodePos.x;
            float y = finalPath[currentNodeIndex].nodePos.z;

            Vector3 currentNodePos = new Vector3(finalPath[currentNodeIndex].nodePos.x, 0, finalPath[currentNodeIndex].nodePos.z);

            Vector3 move = new Vector3(finalPath[currentNodeIndex].nodePos.x - CoWorker.transform.position.x, 0, finalPath[currentNodeIndex].nodePos.z - CoWorker.transform.position.z);//new Vector3(x,1, y);

            //move.Normalize();

            //controller.Move(move * speed * Time.deltaTime);

            CoWorker.transform.position = Vector3.MoveTowards(CoWorker.transform.position, currentNodePos, speed * Time.deltaTime);

            Debug.Log(finalPath[currentNodeIndex].nodePos);
            //Debug.Log("Coworker Moving to:" + move);

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

    void GetWaypoint()
    {




        Debug.Log("Get final path");
        finalPath = PathfindingMgr.inst.FindPathCW(CoWorker.transform.position, roomWaypoints[randomPoint].transform.position);

       


        //checking the points coworker receives

        foreach (NodeMgr n in finalPath)
        {
            Debug.Log(n.nodePos);
        }


    }

    void ReachedWaypoint()
    {
        if (Vector3.Distance(roomWaypoints[randomPoint].transform.position, CoWorker.transform.position) < 10)
        {

            randomPoint = Random.Range(0, roomWaypoints.Count - 1);
        }
    }


    //checks if player enter detection zone
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.name == "Player" && isSnitch)
        {
            boss.Snitch();
        }

    }

    /*
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
    */

}
