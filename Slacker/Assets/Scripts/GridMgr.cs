using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMgr : MonoBehaviour
{
    public Transform startPos; //this is the start of pathfinding location
    public LayerMask wallMask; //this is the mask that the program looks for when trying to find obstructions in path
    public Vector2 gridSize;//store width and heigth of grid
    public float nodeRadius;//how big each sqaure is
    public float distanceBetweenNodes;//distance that grid sqaures spawn from each other

    NodeMgr[,] nodeArray;//array of nodes for A star

    public List<NodeMgr> finalPath;

    float nodeDiameter;//twice the radius
    int gridWidth;//width of grid
    int gridHeight;//heigh of grid

    

    // Start is called before the first frame update
    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridWidth = Mathf.RoundToInt(gridSize.x / nodeDiameter);//divide grid word coords by diameter to get size of graph in array units
        gridHeight = Mathf.RoundToInt(gridSize.y / nodeDiameter);//^

        CreateGrid();//draw grid
    }

    // Update is called once per frame
    void Update()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        nodeArray = new NodeMgr[gridWidth, gridHeight];//declare nodes
        Vector3 bottomLeft = transform.position - Vector3.right * gridSize.x / 2 - Vector3.forward * gridSize.y / 2; //get world position of bottom left of grid
        for (int x = 0; x < gridWidth; x++)//loop through array of nodes
        {
            for (int y = 0; y < gridHeight; y++)//loop through array of nodes
            {
                Vector3 worldPosition = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);//world coords of bottom left
                bool wall = true;//make the node a wall

                //if node is obstructed
                //quick check of collision with current node
                //if colliding with wall
                if(Physics.CheckSphere(worldPosition, nodeRadius, wallMask))
                {
                    wall = false;//object not a wall
                }

                nodeArray[x, y] = new NodeMgr(wall, worldPosition, x, y);//create a new node in array
            }
        }
    }

    //draws gizmo wireframe
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, 1, gridSize.y));//draw a wire cube
        if (nodeArray !=null)//if grid is not empty
        {
            foreach (NodeMgr n in nodeArray)
            {
                if (n.isWall)//if node is a wall
                {
                    Gizmos.color = Color.red;
                }
                else//not a wall
                {
                    Gizmos.color = Color.blue;
                }

                if(finalPath != null)//if final path not empty
                {
                    if (finalPath.Contains(n))//if node is in final path
                    {
                        Gizmos.color = Color.green;
                    }
                }

                Gizmos.DrawCube(n.nodePos, Vector3.one * (nodeDiameter - distanceBetweenNodes));//draw node at node position
            }
        }
    }

    //gets neighbor nodes of given node
    public List<NodeMgr> getNeighbors(NodeMgr givenNode)
    {
        List<NodeMgr> neighborList = new List<NodeMgr>();//list of all neighbors
        int checkX;//used to check if posX is in range of node array
        int checkY;//used to check if posY is in range of node array

        /////////////////
        ///code below could probably be optimized
        ////////////////

        //check the right side of the current node
        checkX = givenNode.gridX + 1;
        checkY = givenNode.gridY;
        if (checkX >= 0 && checkX < gridWidth)//if in range
        {
            if (checkY >=0 && checkY < gridHeight)//if in range
            {
                neighborList.Add(nodeArray[checkX, checkY]);//add to available neighbors
            }
        }

        //check the left side of the current node
        checkX = givenNode.gridX - 1;
        checkY = givenNode.gridY;
        if (checkX >= 0 && checkX < gridWidth)//if in range
        {
            if (checkY >= 0 && checkY < gridHeight)//if in range
            {
                neighborList.Add(nodeArray[checkX, checkY]);//add to available neighbors
            }
        }

        //check the top side of the current node
        checkX = givenNode.gridX;
        checkY = givenNode.gridY + 1;
        if (checkX >= 0 && checkX < gridWidth)//if in range
        {
            if (checkY >= 0 && checkY < gridHeight)//if in range
            {
                neighborList.Add(nodeArray[checkX, checkY]);//add to available neighbors
            }
        }

        //check the bottom side of the current node
        checkX = givenNode.gridX;
        checkY = givenNode.gridY - 1;
        if (checkX >= 0 && checkX < gridWidth)//if in range
        {
            if (checkY >= 0 && checkY < gridHeight)//if in range
            {
                neighborList.Add(nodeArray[checkX, checkY]);//add to available neighbors
            }
        }

        return neighborList;//return list of neighbors
    }

    //gets closest node from given position
    public NodeMgr nodeFromPoint(Vector3 point)
    {
        float xPos = ((point.x + gridSize.x / 2) / gridSize.x);
        float yPos = ((point.y + gridSize.y / 2) / gridSize.y);

        xPos = Mathf.Clamp01(xPos);
        yPos = Mathf.Clamp01(yPos);

        int x = Mathf.RoundToInt((gridWidth - 1) * xPos);
        int y = Mathf.RoundToInt((gridHeight - 1) * yPos);

        return nodeArray[x, y];
    }
}
