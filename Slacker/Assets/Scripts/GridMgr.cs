using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMgr : MonoBehaviour
{

    public Transform startPosition;//start position for pathfinding
    public LayerMask WallMask;//layer mask for walls
    public Vector2 gridSize;//store width and height of grid
    public float nodeRadius;//radius of node
    public float disBetweenNodes;//distance between squares

    NodeMgr[,] nodeArray;//The array of nodes that the A Star algorithm uses.
    public List<NodeMgr> finalPath;//final path of a star


    float nodeDiameter;
    int sizeX;//size of width
    int sizeY;//Size of heigh



  

    private void Start()
    {


        nodeDiameter = nodeRadius * 2;
        sizeX = Mathf.RoundToInt(gridSize.x / nodeDiameter);//Divide the grid coord by the diameter to get the size of the graph in array units.
        sizeY = Mathf.RoundToInt(gridSize.y / nodeDiameter);//^^
       
        CreateGrid();//create grid
    }

    private void Update()
    {
        CreateGrid();
       
    }

    void CreateGrid()
    {
        nodeArray = new NodeMgr[sizeX, sizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * gridSize.x / 2 - Vector3.forward * gridSize.y / 2;//get position of bottom left of grid
        for (int x = 0; x < sizeX; x++)//Loop through the array of nodes.
        {
            for (int y = 0; y < sizeY; y++)//Loop through the array of nodes
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);//get coord of bottom left
                bool Wall = true;//make wall

                //checks if it is not being obstructed by wall
                if (Physics.CheckSphere(worldPoint, nodeRadius, WallMask))
                {
                    Wall = false;//Object is not a wall
                }

                nodeArray[x, y] = new NodeMgr(Wall, worldPoint, x, y);//Create a new node in the array.
            }
        }
    }

    //gets neighbor nodes
    public List<NodeMgr> GetNeighboringNodes(NodeMgr neighbor)
    {
        List<NodeMgr> neighborList = new List<NodeMgr>();//list of neighbors
        int checkX;//checks if x is in range
        int checkY;//checks if y is in range

        //Check the right side of the current node.
        checkX = neighbor.gridX + 1;
        checkY = neighbor.gridY;
        if (checkX >= 0 && checkX < sizeX)//if in range
        {
            if (checkY >= 0 && checkY < sizeY)//if in range
            {
                neighborList.Add(nodeArray[checkX, checkY]);//Add the grid to the available neighbors list
            }
        }
        //Check the Left side of the current node.
        checkX = neighbor.gridX - 1;
        checkY = neighbor.gridY;
        if (checkX >= 0 && checkX < sizeX)///if in range
        {
            if (checkY >= 0 && checkY < sizeY)//if in range
            {
                neighborList.Add(nodeArray[checkX, checkY]);//Add the grid to the available neighbors list
            }
        }
        //Check the Top side of the current node.
        checkX = neighbor.gridX;
        checkY = neighbor.gridY + 1;
        if (checkX >= 0 && checkX < sizeX)///if in range
        {
            if (checkY >= 0 && checkY < sizeY)//if in range
            {
                neighborList.Add(nodeArray[checkX, checkY]);//Add the grid to the available neighbors list
            }
        }
        //Check the Bottom side of the current node.
        checkX = neighbor.gridX;
        checkY = neighbor.gridY - 1;
        if (checkX >= 0 && checkX < sizeX)///if in range
        {
            if (checkY >= 0 && checkY < sizeY)//if in range
            {
                neighborList.Add(nodeArray[checkX, checkY]);//Add the grid to the available neighbors list
            }
        }

        return neighborList;//Return the neighbors list.
    }

    //Gets the closest node to the given world position.
    public NodeMgr GetClosestNode(Vector3 point)
    {
        float xPos = ((point.x + gridSize.x / 2) / gridSize.x);
        float yPos = ((point.z + gridSize.y / 2) / gridSize.y);

        xPos = Mathf.Clamp01(xPos);
        yPos = Mathf.Clamp01(yPos);

        int x = Mathf.RoundToInt((sizeX - 1) * xPos);
        int y = Mathf.RoundToInt((sizeY - 1) * yPos);

        return nodeArray[x, y];
    }


    //Function that draws the wireframe
    private void OnDrawGizmos()
    {

        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, 1, gridSize.y));//Draw a wire cube with the given dimensions 

        if (nodeArray != null)
        {
            foreach (NodeMgr n in nodeArray)//Loop through every node in the grid
            {
                if (n.isWall)//If the current node is a wall node
                {
                    Gizmos.color = Color.white;//Set the color to white
                }
                else
                {
                    Gizmos.color = Color.blue;//Set the color to blue
                }


                if (finalPath != null)//If the final path is not empty
                {
                    if (finalPath.Contains(n))//If the current node is in the final path
                    {
                        Gizmos.color = Color.red;//Set the color to red
                    }

                }


                Gizmos.DrawCube(n.nodePos, Vector3.one * (nodeDiameter - disBetweenNodes));//draw node

            }
        }
    }
}
