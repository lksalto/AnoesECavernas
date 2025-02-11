using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemiesPrefabs;
    public float startTimer = 5;
    public bool end;
////////////////////////////////////////////////////////////////////
    public Waves[] waves;

    [System.Serializable] 
    public struct Waves
    {
        public EnemyQtt[] EnemiesQts;
        public float CoolDownBetweenEnemys;
        public float SecsToNewWave;
        public bool WhenClear;
    }
    [System.Serializable]
    public struct EnemyQtt
    {
        public int enemyId;
        public int enemyQt;
        public int pathId;
        public bool RandomPath;
    }
    void Start()
    {
        end= false;
        StartCoroutine(waitToStart(startTimer));
    }

    IEnumerator waitToStart(float sec)
    {
        yield return new WaitForSeconds(sec);
        StartCoroutine(spawnEnemy(enemiesPrefabs, waves));
    }

//Spawnar "qtt" inimigos do tipo "enemyList[idx]", a cada "cd" 
    IEnumerator spawnEnemy(List<GameObject> enemyList,Waves[] waves)
    {
        for(int i = 0; i < waves.Length; i++) 
        {
            int qts = 0;
            int[] ids = new int[waves[i].EnemiesQts.Length];
            int[] idsQt= new int[waves[i].EnemiesQts.Length];
            int[] idsPath = new int[waves[i].EnemiesQts.Length];
            for (int j=0; j < waves[i].EnemiesQts.Length; j++) 
            {
                qts += waves[i].EnemiesQts[j].enemyQt;
                ids[j] = waves[i].EnemiesQts[j].enemyId;
                idsQt[j] = waves[i].EnemiesQts[j].enemyQt;
                if (!waves[i].EnemiesQts[j].RandomPath) { idsPath[j] = waves[i].EnemiesQts[j].pathId; }
                else { idsPath[j] = -1; }
                //Debug.Log("id:" + ids[j].ToString());
                //Debug.Log("qt:" + idsQt[j].ToString());
            }
            for (int j=0; j < qts; j++) 
            {
                int aux = Random.Range(0, ids.Length);
                //Debug.Log("aux:"+aux.ToString());
                //Debug.Log("idsQt[aux] antes:" + idsQt[aux].ToString());
                if (idsQt[aux] > 0)
                {
                    idsQt[aux] -=1;
                    //Debug.Log("idsQt[aux] depois:" + idsQt[aux].ToString());

                    GameObject Enemy = Instantiate(enemyList[ids[aux]], transform.position, Quaternion.identity);
                    Enemy.GetComponent<EnemyPathing>().pathIdx = idsPath[aux];
                    //Debug.Log("Spawner" + idsPath[aux].ToString());
                    Enemy.transform.parent = null;


                    if (idsQt[aux] == 0)
                    {
                        Debug.Log("idsQt[aux] depois:");
                        int[] auxid = new int[ids.Length-1];
                        int[] auxidq = new int[idsQt.Length - 1];
                        int[] auxidpath = new int[idsQt.Length - 1];
                        int ki=0;
                        for (int k=0; k<ids.Length; k++) 
                        {
                            if (idsQt[k] != 0) 
                            {
                                auxid[ki] = ids[k];
                                auxidq[ki] = idsQt[k];
                                auxidpath[ki] = idsPath[k];
                                ki++;
                            }
                        }
                        ids = auxid;
                        idsQt = auxidq;
                        idsPath = auxidpath;
                    }

                    yield return new WaitForSeconds(waves[i].CoolDownBetweenEnemys);
                }
            }

            if (waves[i].WhenClear) 
            {
                while (FindObjectsOfType<EnemyPathing>().Length > 0)
                {
                    yield return null; // Waits for the next frame instead of freezing
                }
            }
            yield return new WaitForSeconds(waves[i].SecsToNewWave);
        }
        end = true;
    }
}
