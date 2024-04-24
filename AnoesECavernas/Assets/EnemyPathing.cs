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
        //Busca todos os caminhos criados
        allPaths = GameObject.FindGameObjectsWithTag("Path");
        //Escolhe qual caminho a seguir (aleatoriamente)
        pathIdx = Random.Range(0, allPaths.Length); 
        //Criando o caminho
        foreach (Transform wp in allPaths[pathIdx].GetComponentInChildren<Transform>())
        {
            waypoints.Add(wp);
        }
        //Se quiser, da pra deixar o primeiro ponto com o spawn do inimigo
        transform.position = waypoints[nextWaypoint].position;
    }

    
    void Update()
    {
       GoToNextWp();
    }

    //Ir para o proximo Waypoint
    void GoToNextWp()
    {
        //Enqt não chegamos no ultimo waypoint
        if(nextWaypoint < waypoints.Count)
        {
            //mover o inimigo
            transform.position = Vector2.MoveTowards(transform.position, waypoints[nextWaypoint].position, speed*Time.deltaTime);
            //se chegou no waypoint
            if (transform.position == waypoints[nextWaypoint].position)
            {
                //ir pro proximo
                nextWaypoint++;
            }
        }
        else //chegou ao fim (vamos colocar aqui para ele dar dano no jogador)
        {
            
            Destroy(gameObject);
        }

    }
}
