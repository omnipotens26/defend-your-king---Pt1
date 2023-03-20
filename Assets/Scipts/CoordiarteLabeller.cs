using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordiarteLabeller : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.grey; 

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    Waypoint waypoint;

    void Awake() {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        DisplayCoordinate();
        waypoint = GetComponentInParent<Waypoint>();
    }

    void Update()
    {
        if (!Application.isPlaying){
            DisplayCoordinate();
            UpdateObjectName();
        }
        SetLabelColor();
        ToggleLables();
    }

    private void SetLabelColor()
    {
        if (waypoint.IsPlaceable){
            label.color = defaultColor;
        } else {
            label.color = blockedColor;
        }
    }

    void ToggleLables(){
        if (Input.GetKeyDown(KeyCode.C)){
            label.enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.C)){
            label.enabled = false;
        }
    }
/*
    Si la méthode DisplayCoordinate() renvoie une erreur pendant le build, regarder le cours 130 (Refactoring) aux alentours de 11min
*/
    private void DisplayCoordinate() 
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
        label.text =  coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectName(){
        transform.parent.name = coordinates.ToString();
    }
}
