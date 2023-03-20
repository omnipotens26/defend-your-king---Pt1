using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>(); //liste des coordonnées à suivre
    [SerializeField] [Range(0f , 5f)] float speed = 1f;

    Enemy enemy;

    void OnEnable() {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    private void Start() {
        enemy = GetComponent<Enemy>();
    }
    

    //Dans la lecture 122 il nous dit qu'il faudrait changer plus tard, donc je l'ai fait mainteanant
    //4min34 lecture 122 pour explications du problème
    void FindPath(){
        path.Clear();
        GameObject parent = GameObject.FindGameObjectWithTag("Path");
        foreach (Transform child in parent.transform){
            Waypoint waypoint = child.GetComponent<Waypoint>();

            if (waypoint != null){
                path.Add(waypoint);
            }
        }
    }

    void ReturnToStart(){
        transform.position = path[0].transform.position;
    }

    IEnumerator FollowPath(){
        foreach (Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position; 
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while(travelPercent < 1){   //tant que nous ne sommes pas a notre endPosition
                travelPercent += Time.deltaTime * speed;

                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent); // ex: startPosition = 2;0 endPosition = 3;0
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }

    void FinishPath(){
        enemy.StealGold();
        gameObject.SetActive(false);
    }

}
