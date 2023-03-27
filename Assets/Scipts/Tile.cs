using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab; 
    [SerializeField] bool isPlaceable;  //isPlaceable a été défini a TRUE par défault dans Unity

    GridManager gridManager;
    Pathfinder pathfinder;
    Vector2Int coordinates = new Vector2Int();

    void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();

    }

    void Start() {
        
        if(gridManager != null)
        {
            coordinates = gridManager.getCoordinatesFromPosition(transform.position);
            
            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }
    
    public bool IsPlaceable{ get { return isPlaceable; } }

    // MON GETTER
    // public bool GetIsPlaceable(){
    //     return isPlaceable;
    // }

    private void OnMouseDown() {
        if (gridManager.getNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates))
        {
            bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position);
            if (isSuccessful)
            {
                gridManager.BlockNode(coordinates);
                pathfinder.NotifyReceivers();
            }
        }
    }   
}
