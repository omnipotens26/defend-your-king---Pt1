using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
  [Tooltip("World grid size should match UnityEditor snap settings")]
  [SerializeField] int unityGridSize = 10;
  [SerializeField] Vector2Int gridSize; //Specify grid size

  Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
  
  public int UnityGridSize { get { return unityGridSize; } }
  public Dictionary<Vector2Int, Node> Grid { get  { return grid; } }

  private void Awake() {
    CreateGrid();
  }

  public Node getNode(Vector2Int coordinates)
  {
    if (grid.ContainsKey(coordinates))
    {
      return grid[coordinates];
    }
    return null;
  }

/*
  For the specified size of the grid (via SerializedField),
  Creates a V2int called "coordinates"
  & use this attribute to create the key && the Node
                        -> the Node contains -the coordinates   -isWalkable
*/
  private void CreateGrid()
  {
    for (int x = 0; x < gridSize.x; x++){
      for (int y = 0; y < gridSize.y; y++){
          Vector2Int coordinates = new Vector2Int(x,y);
          grid.Add(coordinates, new Node(coordinates, true));
      }
    }
  }

  public void BlockNode(Vector2Int coordinates)
  {
      if(grid.ContainsKey(coordinates))
      {
          grid[coordinates].isWalkable = false;
      }
  }

  public void ResetNodes()
  {
    foreach(KeyValuePair<Vector2Int, Node> entry in grid){
      entry.Value.connectedTo = null;
      entry.Value.isExplored = false;
      entry.Value.isPath = false;
    }
  }

  public Vector2Int getCoordinatesFromPosition(Vector3 position)
  { 
    Vector2Int coordinates = new Vector2Int();
    coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
    coordinates.y = Mathf.RoundToInt(position.z / unityGridSize);

    return coordinates;
  }

  public Vector3 getPositionFromCoordinates(Vector2Int coordinates)
  {
    Vector3 position = new Vector3();
    position.x = (coordinates.x * unityGridSize);
    position.z = (coordinates.y * unityGridSize);

    return position;
  }

    
}
