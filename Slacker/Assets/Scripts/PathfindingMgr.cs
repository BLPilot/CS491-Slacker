using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingMgr : MonoBehaviour
{
    GridMgr gridReference;//reference to grid class
    public Transform startPosition;//start position of pathfind
    public Transform targetPosition;//target position of pathfind

    private void Awake()
    {
        gridReference = GetComponent<GridMgr>();//get reference to grid
    }

    // Update is called once per frame
    void Update()
    {
        FindPath(startPosition.position, targetPosition.position);//find path to target
    }

    void FindPath(Vector3 sPos, Vector3 tPos)
    {
        NodeMgr startNode = gridReference.nodeFromPoint(sPos);//gets node closest to start
        NodeMgr targetNode = gridReference.nodeFromPoint(tPos);//gets node closest from target

        List<NodeMgr> openList = new List<NodeMgr>();
        HashSet<NodeMgr> closedList = new HashSet<NodeMgr>();

        openList.Add(startNode);//add starting node to open list to start program

        while (openList.Count > 0)//while there is something in open list
        {
            NodeMgr currentNode = openList[0];//create a node and set it to first item
            for (int i = 1; i < openList.Count; i++)//loop through open list
            {
                if (openList[i].FCost < currentNode.FCost || openList[i].FCost == currentNode.FCost && openList[i].hCost < currentNode.hCost)
                {
                    currentNode = openList[i];//set the current node to object
                }
            }

            openList.Remove(currentNode);//remove from open list
            closedList.Add(currentNode);//add to closed list

            if (currentNode == targetNode)//if node is same as target node
            {
                GetFinalPath(startNode, targetNode);//get final path
            }

            foreach (NodeMgr neighborNode in gridReference.getNeighbors(currentNode))//loop through each neighbor of current node
            {
                if (!neighborNode.isWall || closedList.Contains(neighborNode))//if neighbor is wall or has already been checked
                {
                    continue;//skip
                }
                int moveCost = currentNode.gCost + GetManhattenDistance(currentNode, neighborNode);//get the f cost of that neightbor

                if(moveCost < neighborNode.gCost || !openList.Contains(neighborNode))//if f cost is greater than the g cost or it is not in open list
                {
                    neighborNode.gCost = moveCost;
                    neighborNode.hCost = GetManhattenDistance(neighborNode, targetNode);
                    neighborNode.parentNode = currentNode;//retrace steps

                    if (!openList.Contains(neighborNode))//if neighbor is not in open list
                    {
                        openList.Add(neighborNode);
                    }
                }
            }
        }
    }

    void GetFinalPath(NodeMgr sNode, NodeMgr tNode)
    {
        List<NodeMgr> finalPath = new List<NodeMgr>();//list to hold path
        NodeMgr currentNode = tNode;

        while(currentNode != sNode)//loop to work through each node to the begining
        {
            finalPath.Add(currentNode);
            currentNode = currentNode.parentNode;
        }

        finalPath.Reverse();//reverse path to get correct order

        gridReference.finalPath = finalPath;//set final path
        
    }

    int GetManhattenDistance(NodeMgr aNode, NodeMgr bNode)
    {
        int x = Mathf.Abs(aNode.gridX - bNode.gridX);//x1-x2
        int y = Mathf.Abs(aNode.gridY - bNode.gridY);//y1-y2

        return x + y;//return sum
    }
}
