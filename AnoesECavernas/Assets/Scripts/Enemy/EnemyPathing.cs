using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] GameObject[] allPaths;
    [SerializeField] GameObject[] nextPath;
    [SerializeField] List<Transform> waypoints;
     int nextWaypoint = 0; //inicializando no primeiro waypoint
    public float distanceTravelled = 0;
    public int dmg;

    PlayerLife playerLife;
    EnemyLife enemyLife;


    [SerializeField]public int pathIdx=-2;
    
    void Start()
    {
        enemyLife = GetComponent<EnemyLife>();
        dmg = enemyLife.dmg;
        //referenciar a vida do jogador ao inimigo
        playerLife = FindObjectOfType<PlayerLife>();

        //Busca todos os caminhos de inicio
        allPaths = GameObject.FindGameObjectsWithTag("Path");
        //Debug.Log("Path" + pathIdx.ToString());

        //Escolhe qual caminho a seguir (aleatoriamente)
        if (pathIdx == -1) { pathIdx = Random.Range(0, allPaths.Length); }
        //Debug.Log("Rand" + pathIdx.ToString());



        //Criando o caminho
        foreach (Transform wp in allPaths[pathIdx].GetComponentInChildren<Transform>())
        {
            waypoints.Add(wp);
        }
        //enquanto o caminho n for indentificado como final
        while (!allPaths[pathIdx].GetComponent<WaypointChanger>().end)
        {
            //indentifica os proximos possiveis caminhos
            allPaths = allPaths[pathIdx].GetComponent<WaypointChanger>().nextpath;
            // escolhe um caminho aleatorio
            pathIdx = Random.Range(0, allPaths.Length);
            //adciona no caminho geral
            foreach (Transform wp in allPaths[pathIdx].GetComponentInChildren<Transform>())
            {
                waypoints.Add(wp);
            }
        }
        //Se quiser, da pra deixar o primeiro ponto com o spawn do inimigo
        transform.position = waypoints[nextWaypoint].position;
    }


    void Update()
    {
        GoToNextWp();

        CalculateDistance();
    }
    //Ir para o proximo Waypoint
    void GoToNextWp()
    {
        //Enqt não chegamos no ultimo waypoint
        if(nextWaypoint < waypoints.Count)
        {
            //mover o inimigo
            transform.position = Vector2.MoveTowards(transform.position, waypoints[nextWaypoint].position, enemyLife.speed*Time.deltaTime);
            //se chegou no waypoint
            if (transform.position == waypoints[nextWaypoint].position)
            {
                    //ir pro proximo
                    nextWaypoint++;
            }
        }
        else //chegou ao fim (vamos colocar aqui para ele dar dano no jogador)
        {
            playerLife.playerTakeDamage(dmg);
            enemyLife.CheckGameOver();
            Destroy(gameObject);
        }

    }
    void CalculateDistance()
    {
        distanceTravelled += Time.deltaTime * enemyLife.speed;
    }

}
