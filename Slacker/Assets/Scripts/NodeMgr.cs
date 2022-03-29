using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMgr
{
    public int gridX;//X position in the node array
    public int gridY;//Y position in the node array

    public bool isWall;//checks if node is being obstructed
    public Vector3 nodePos;//world position of node

    public NodeMgr parentNode;//Stores the parent node so it can trace shortest path

    public int gCost;//cost of moving to next sqaure
    public int hCost;//distance from goal to curent node

    public int FCost { get { return gCost + hCost; } }//get function to add gCost and hCost

    public NodeMgr(bool _isWall, Vector3 _nodePos, int _gridX, int _gridY)//Node Constructor
    {
        isWall = _isWall;
        nodePos = _nodePos;
        gridX = _gridX;
        gridY = _gridY;
    }
    
}
