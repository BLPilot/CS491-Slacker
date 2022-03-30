using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingMgr : MonoBehaviour
{

    GridMgr gridReference;//reference to the grid class
    public Transform startPosition;//Starting position to pathfind from
    public Transform targetPosition;//target position for pathfind


    private void Awake()
    {
        gridReference = GetComponent<GridMgr>();//Get a reference to the game manager
    }





    private void Update()
    {
        FindPath(startPosition.position, targetPosition.position);//Find a path to the goal
    }

    void FindPath(Vector3 sPos, Vector3 tPos)
    {
        NodeMgr startNode = gridReference.GetClosestNode(sPos);//Gets the node closest to the starting position
        NodeMgr targetNode = gridReference.GetClosestNode(tPos);//Gets the node closest to the target position

        List<NodeMgr> OpenList = new List<NodeMgr>();//List of nodes for the open list
        HashSet<NodeMgr> ClosedList = new HashSet<NodeMgr>();//closed list

        OpenList.Add(startNode);//Add the starting node to the open list

        while (OpenList.Count > 0)//While something in open list
        {
            NodeMgr currentNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
            {
                if (OpenList[i].FCost < currentNode.FCost || OpenList[i].FCost == currentNode.FCost && OpenList[i].hCost < currentNode.hCost)//If the f cost of that object is less than or equal to the f cost of the current node
                {


                    currentNode = OpenList[i];
                }
            }
            OpenList.Remove(currentNode);//Remove that from the open list
            ClosedList.Add(currentNode);//And add it to the closed list

            if (currentNode == targetNode)//If current node is same as target
            {
                GetFinalPath(startNode, targetNode);//get final path
            }

            foreach (NodeMgr neighborNode in gridReference.GetNeighboringNodes(currentNode))//traverse neighbors
            {
                if (!neighborNode.isWall || ClosedList.Contains(neighborNode))//If the neighbor is a wall or has already checked
                {
                    continue;//Skip
                }
                int MoveCost = currentNode.gCost + GetManhattenDistance(currentNode, neighborNode);//Get the F cost of neighbor

                if (MoveCost < neighborNode.gCost || !OpenList.Contains(neighborNode))//If the f cost is greater than the g cost or it is not in the open list
                {
                    neighborNode.gCost = MoveCost;//Set the g cost to the f cost
                    neighborNode.hCost = GetManhattenDistance(neighborNode, targetNode);
                    neighborNode.parentNode = currentNode;//set parent to retrace

                    if (!OpenList.Contains(neighborNode))
                    {
                        OpenList.Add(neighborNode);//Add it to the list
                    }
                }
            }

        }
    }



    void GetFinalPath(NodeMgr sNode, NodeMgr tNode)
    {
        List<NodeMgr> FinalPath = new List<NodeMgr>();//list for final path
        NodeMgr CurrentNode = tNode;//store current ode

        while (CurrentNode != sNode)
        {


            FinalPath.Add(CurrentNode);//add node to final path
            CurrentNode = CurrentNode.parentNode;//move to parent node
        }



        FinalPath.Reverse();//Reverse the path to get the correct order

        gridReference.finalPath = FinalPath;
       


    }

    int GetManhattenDistance(NodeMgr aNode, NodeMgr bNode)
    {
        int x = Mathf.Abs(aNode.gridX - bNode.gridX);//x1-x2
        int y = Mathf.Abs(aNode.gridY - bNode.gridY);//y1-y2

        return x + y;//Return the sum
    }
}
