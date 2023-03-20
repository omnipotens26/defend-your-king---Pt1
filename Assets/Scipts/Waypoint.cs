using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefab; 
    [SerializeField] bool isPlaceable;  //isPlaceable a été défini a TRUE par défault dans Unity
    
    public bool IsPlaceable{ get { return isPlaceable; } }

    // MON GETTER
    // public bool GetIsPlaceable(){
    //     return isPlaceable;
    // }

    private void OnMouseDown() {
        if (isPlaceable){
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            isPlaceable = !isPlaced;
        }
    }
}
