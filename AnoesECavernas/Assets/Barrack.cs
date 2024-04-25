using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : MonoBehaviour
{
    public Tower tower;
    [SerializeField] GameObject target;
    public Transform spawn;

    void Start()
    {
        tower.spawnPoint = spawn;
    }


    void Update()
    {
        tower.detectEnemy(transform.position);
        tower.atkCd -= Time.deltaTime;
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, tower.atkRange);
    }

}
