using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemiesPrefabs;
    public float cooldDown = 1f;
    public int qtt = 100;
    void Start()
    {
        StartCoroutine(spawnEnemy(enemiesPrefabs, 0, qtt, cooldDown));
    }

    //Spawnar "qtt" inimigos do tipo "enemyList[idx]", a cada "cd" segundos
    IEnumerator spawnEnemy(List<GameObject> enemyList, int idx, int qtt,float cd)
    {

        yield return new WaitForSeconds(cd);
        GameObject enemy = Instantiate(enemyList[idx], transform.position, Quaternion.identity);
        enemy.transform.parent = null;
        if(qtt > 0)
        {
            if(Random.Range(0, 10) <= 4)
            {
                idx = 0;
            }
            else
            {
                idx = 1;
            }
            StartCoroutine(spawnEnemy(enemyList, idx, qtt - 1, cd));
        }
    }
}
