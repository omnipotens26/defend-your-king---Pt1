using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public Vector2Int StartCoordinates { get { return startCoordinates; } }
    public Vector2Int DestinationCoordinates { get {return destinationCoordinates; } }

    [SerializeField] Vector2Int startCoordinates;
    [SerializeField] Vector2Int destinationCoordinates;

    Node startNode;
    Node destinationNode;
    Node currentSearchNode;    //node currently focused

    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>(); //used to see whether a node has already be explored
    Queue<Node> frontier = new Queue<Node>();   //all nodes connected to neighbors but not yet explored

    Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};
    GridManager gridManager;    //contains a Dictionnary (.grid) that holds all the nodes
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    /*
        On a besoin d'appeller le gridManager afin de vérifier sur neighborsCoord fait parti
        de notre grille
    */

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();

        if (gridManager != null)
        {
            grid = gridManager.Grid;
        }

        //To get the starting/destination Node, we get the Node from our grid whose key is the "startCoordinates" value
        startNode = grid[startCoordinates]; 
        destinationNode = grid[destinationCoordinates];

        
    }

    void Start()
    {   
        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        /*
        gridManager.ResetNodes();
        BreadthFirstSearch(startCoordinates);
        return BuildPath();
        
            Comment les 2 méthodes GetNewPath() contiennent les mêmes méthodes, on évite la redondance de code
        */
        return GetNewPath(startCoordinates);
    }

    public List<Node> GetNewPath(Vector2Int currentCoordinates)
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(currentCoordinates);
        return BuildPath();
    }

    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();
        for (int i = 0; i < directions.Length; i++)     //mon ex : x=1 y=0  résultats attendus : 2;0 ** 0;0 ** 1;1 ** 1;-1
        {
            Vector2Int neighborCoords = currentSearchNode.coordinates + directions[i];
            if (grid.ContainsKey(neighborCoords)){
                neighbors.Add(grid[neighborCoords]);        //we add the values of the Node corresponding to the key 
            }
        }

        foreach (Node neighbor in neighbors)
        {
            //check if the Node is already in the "reached" dictionnary & if the node isWalkable
            if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                neighbor.connectedTo = currentSearchNode;
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }
    /*
    Steps of the search : 
        -Add the starting node to our Queue & fill Dictionnary "reached" with the starting values 
        -
    */
    void BreadthFirstSearch(Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        destinationNode.isWalkable = true;

        frontier.Clear();
        reached.Clear();

        bool isRunning = true;  //help for breakout our while loop
        frontier.Enqueue(grid[coordinates]);
        reached.Add(coordinates, grid[coordinates]);

        while(frontier.Count > 0 && isRunning){
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();

            if (currentSearchNode.coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }
        path.Reverse();
        return path;
    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates)){
            bool previousState = grid[coordinates].isWalkable;

            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();

            grid[coordinates].isWalkable = previousState;

            if(newPath.Count <= 1){
                GetNewPath();
                return true;
            }
        }
        return false;
    }

    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }
}
