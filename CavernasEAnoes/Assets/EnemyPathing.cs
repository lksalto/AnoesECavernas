using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] GameObject[] allPaths;
    [SerializeField] List<Transform> waypoints;
    [SerializeField] int nextWaypoint = 0; //inicializando no primeiro waypoint


    public float speed = 10f;

    [SerializeField] int pathIdx;
    
    void Awake()
    {
        allPaths = GameObject.FindGameObjectsWithTag("Path");
        pathIdx = Random.Range(0, allPaths.Length); //qual caminho a seguir
        //criando o caminho
        foreach (Transform wp in allPaths[pathIdx].GetComponentInChildren<Transform>())
        {
            waypoints.Add(wp);
        }
        //transform.position = waypoints[nextWaypoint].position;
    }

    
    void Update()
    {
       GoToNextWp();
    }

    void GoToNextWp()
    {
        if(nextWaypoint < waypoints.Count)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[nextWaypoint].position, speed*Time.deltaTime);
            if (transform.position == waypoints[nextWaypoint].position)
            {
                nextWaypoint++;
            }
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
