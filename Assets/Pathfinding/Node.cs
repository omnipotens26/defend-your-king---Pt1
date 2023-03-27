using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2Int coordinates;      //coordinates of the node in the grid
    public bool isWalkable;             //determine if the node can be added to the tree
    public bool isExplored;             //hold the state of whether its been explored by our Pathfinding
    public bool isPath;                 //wether the node actually in the node or not
    public Node connectedTo;            //Holds the parent node

    public Node(Vector2Int coordinates, bool isWalkable){
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
    }
}
