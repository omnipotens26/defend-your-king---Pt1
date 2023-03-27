using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnnemyMover : MonoBehaviour
{
    List<Node> path = new List<Node>(); //liste des coordonnées à suivre
    [SerializeField] [Range(0f , 5f)] float speed = 1f;

    Enemy enemy;
    Pathfinder pathfinder;
    GridManager gridManager;

    void OnEnable() {
        ReturnToStart();
        RecalculatePath(true);
        
    }

    private void Awake() {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }
    

    //Dans la lecture 122 il nous dit qu'il faudrait changer plus tard, donc je l'ai fait mainteanant
    //4min34 lecture 122 pour explications du problème
    void RecalculatePath(bool resetPath)
    {
        Vector2Int tempCoordinates = new Vector2Int();

        if(resetPath)
        {
            tempCoordinates = pathfinder.StartCoordinates;
        } else 
        {
            tempCoordinates = gridManager.getCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(tempCoordinates);
        StartCoroutine(FollowPath());
    }

    void ReturnToStart()
    {
        
        transform.position = gridManager.getPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.getPositionFromCoordinates(path[i].coordinates); 
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

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

}
