using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemiesPrefabs;
    public float coolDown = 1f;
    public int qtty;
    public int initialQtt;
    public float startTimer = 5;
    public float mult = 2;
    void Start()
    {
        initialQtt = qtty;
        StartCoroutine(waitToStart(startTimer));
    }

    IEnumerator waitToStart(float sec)
    {
        yield return new WaitForSeconds(sec);
        StartCoroutine(spawnEnemy(enemiesPrefabs, 0, qtty, coolDown));
    }

//Spawnar "qtt" inimigos do tipo "enemyList[idx]", a cada "cd" 
    IEnumerator spawnEnemy(List<GameObject> enemyList, int idx, int qtt,float cd)
    {
        qtty = qtt;
        yield return new WaitForSeconds(cd);
        GameObject enemy = Instantiate(enemyList[idx], transform.position, Quaternion.identity);
        enemy.transform.parent = null;
        //hard
        if (qtt == initialQtt/4)
        {
            GameObject fran = Instantiate(enemyList[4], transform.position, Quaternion.identity);
        }
        if(qtt < initialQtt/4)
        {
            
            cd = coolDown / mult;
            if (Random.Range(0, 15) < 3)
            {
                idx = 0;
            }
            else if (Random.Range(0, 12) < 7)
            {
                idx = 1;
            }
            else if (Random.Range(0, 12) < 10)
            {
                idx = 2;
            }
            else
            {
                idx = 3;
            }

        }
        //medium
        else if(qtt < initialQtt/2)
        {
            
            cd = coolDown / 2;
            if (Random.Range(0, 12) < 5)
            {
                idx = 0;
            }
            else if(Random.Range(0, 12) < 10)
            {
                idx = 1;
            }
            else
            {
                idx = 2;
            }
        }
        else //easy
        {
            
            if (Random.Range(0, 10) < 5)
            {
                idx = 0;
            }
            else
            {
                idx = 1;
            }
        }
        if(qtt>=0)
        {
            StartCoroutine(spawnEnemy(enemyList, idx, qtt - 1, cd));
        }

        
    }
}
