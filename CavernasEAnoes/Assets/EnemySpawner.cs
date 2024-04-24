using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemiesPrefabs;
    public float cd = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(enemiesPrefabs, 0, 10));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnEnemy(List<GameObject> enemyList, int idx, int qtt)
    {

        yield return new WaitForSeconds(cd);
        Instantiate(enemyList[idx], transform.position, Quaternion.identity);
        if(qtt > 0)
        {
            StartCoroutine(spawnEnemy(enemyList, idx, qtt-1));
        }
    }
}
