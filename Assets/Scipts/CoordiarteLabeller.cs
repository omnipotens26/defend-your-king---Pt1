using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordiarteLabeller : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.grey;
    [SerializeField] Color blockedColor = Color.black; 
    [SerializeField] Color exploredColor = Color.red;
    [SerializeField] Color pathColor = Color.magenta;


    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    // Waypoint waypoint;

    GridManager gridManager;

    void Awake() {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        
        // waypoint = GetComponentInParent<Waypoint>();
        gridManager = FindObjectOfType<GridManager>();
        DisplayCoordinate();
        
    }

    void Update()
    {
        if (!Application.isPlaying){
            DisplayCoordinate();
            UpdateObjectName();
            label.enabled = true;
        }
        SetLabelColor();
        ToggleLabels();
    }

    private void SetLabelColor()
    {
              
        if (gridManager == null) { return; } //si gridManager n'existe pas, on ne continue pas la méthode

        Node node = gridManager.getNode(coordinates);

        if (node == null) { return; }
        
        if (!node.isWalkable){
            label.color = blockedColor;
        } else if (node.isPath) {
            label.color = pathColor;
        } else if (node.isExplored) {
            label.color = exploredColor;
        } else {
            label.color = Color.white;
        }
        
        // if (waypoint.IsPlaceable){
        //     label.color = defaultColor;
        // } else {
        //     label.color = blockedColor;
        // }
    }

    void ToggleLabels(){
        // if (Input.GetKeyDown(KeyCode.C)){
        //     label.enabled = true;
        // }
        // if (Input.GetKeyUp(KeyCode.C)){
        //     label.enabled = false;
        // }
        if(Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }

    }
/*
    Si la méthode DisplayCoordinate() renvoie une erreur pendant le build, regarder le cours 130 (Refactoring) aux alentours de 11min
*/
    private void DisplayCoordinate() 
    {
        if(gridManager == null) { return; }
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);
        label.text =  coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectName(){
        transform.parent.name = coordinates.ToString();
    }
}
